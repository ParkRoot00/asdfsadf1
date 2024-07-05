using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class Client : MonoBehaviour
{
    private const int m_port = 44444;
    private Socket m_socket = null;

    // Start is called before the first frame update
    public void JoinBtn()
    {

        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_socket.NoDelay = true;
        m_socket.SendBufferSize = 0;
        m_socket.Connect("127.0.0.1", m_port);

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("H E L L O");
        m_socket.Send(buffer, buffer.Length, SocketFlags.None);
        m_socket.Shutdown(SocketShutdown.Both);
        m_socket.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
}