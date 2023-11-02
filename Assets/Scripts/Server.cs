using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class Server : MonoBehaviour
{
    private const int m_port = 44444;
    private Socket m_listener = null;
    private Socket m_socket = null;
    private State m_state;
    private int cnt;

    enum State
    {
        idle = 0,
        StartListener,
        AcceptClient,
        ServerCommunication,
        StopListener,
        EndCommunication,
    }

    void Start()
    {
        cnt = 0;
        m_state = State.idle;
        /* 이거 아이피 가져오는 코드였음
        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress hostAddress = hostEntry.AddressList[0];
        Debug.Log("My IP : " + hostAddress.MapToIPv4().ToString());  
        */
    }

    public void startBtn()
    {
        if(cnt < 1)
        {
            m_state = State.StartListener;

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Debug.Log(ip.ToString());
                }
            }
            cnt += 1;
        }
        else
        {
            Debug.Log("안됨 나가");
        }
    }

    void StartListener()
    {
        m_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_listener.Bind(new IPEndPoint(IPAddress.Any, m_port));
        m_listener.Listen(1);
        Debug.Log("Start Listening");
        m_state = State.AcceptClient;

    }

    void Update()
    {
        switch (m_state)
        {
            case State.StartListener:
                StartListener();
                break;
            case State.AcceptClient:
                AcceptClient();
                break;
            case State.ServerCommunication:
                ServerCommunication();
                break;
            case State.StopListener:
                StopListener();
                break;
            case State.EndCommunication:
                break;
            default:
                break;
        }
    }

    void AcceptClient()
    {
        if (m_listener != null && m_listener.Poll(0, SelectMode.SelectRead))
        {
            m_socket = m_listener.Accept();
            Debug.Log("TCP CONNECTED");

            m_state = State.ServerCommunication;
        }
    }

    void ServerCommunication()
    {
        byte[] buffer = new byte[1400];
        int recvSize = m_socket.Receive(buffer, buffer.Length, SocketFlags.None);

        if (recvSize > 0)
        {
            string msg = System.Text.Encoding.UTF8.GetString(buffer);
            Debug.Log(msg);
            m_state = State.StopListener;

        }
    }

    void StopListener()
    {
        if (m_listener != null)
        {
            m_listener.Close();
            m_listener = null;
        }

        m_state = State.EndCommunication;
    }
}