using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawWithMouse00 : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPosition;

    [SerializeField]
    private float minDistance = 0.1f;
    [SerializeField,Range(0,1)]
    private float width;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        previousPosition = transform.position;
        line.startWidth = line.endWidth = width;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
            {

                if (previousPosition == transform.position)
                {
                    // it means its the first point
                    line.SetPosition(0, currentPosition);
                }
                else
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, currentPosition);
                }
                previousPosition = currentPosition;
            }
        }
    }

}
