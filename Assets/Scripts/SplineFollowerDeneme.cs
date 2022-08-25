using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SplineFollowerDeneme : MonoBehaviour
{
    private SplineFollower sf;
    void Start()
    {
        sf = GetComponent<SplineFollower>();
        sf.onNode += OnNodePassed;
    }

    private void OnNodePassed(List<SplineTracer.NodeConnection> passed)
    {
        SplineTracer.NodeConnection nodeConnection = passed[0];
        if (nodeConnection.node.name == "Node")
        {
            Debug.Log(nodeConnection.node.name + " at point " + nodeConnection.point);
            Node.Connection[] connections = nodeConnection.node.GetConnections();
            SplinePoint a = nodeConnection.node.GetPoint(0, false);
            Debug.Log(a.position);
            int temp = 0;
            for (int i = 0; i < connections[0].spline.pointCount; i++)
            {
         
                if (connections[0].spline.GetPoint(i).position == a.position)
                {
                    temp = i;
                    Debug.Log(temp);
                    break;
                }
            }
            sf.spline = connections[0].spline;
            sf.SetPercent(sf.spline.GetPointPercent(temp)+0.001d);
            Debug.Log(nodeConnection.node.name + " at point " + nodeConnection.point);
        }
        else if (nodeConnection.node.name == "Node 1")
        {
            Node.Connection[] connections = nodeConnection.node.GetConnections();
            Debug.Log(nodeConnection.node.name + " at point " + nodeConnection.point);
            SplinePoint a = nodeConnection.node.GetPoint(1, false);
            int temp = 0;
            Debug.Log("-s-s-s-s-s-s--ss");
            for (int i = 0; i < connections[1].spline.pointCount; i++)
            {
                Debug.Log(connections[1].spline.GetPoint(i).position + "--" + a.position);
                if (connections[1].spline.GetPoint(i).position == a.position)
                {
                    temp = i;
                    break;
                }
            }
            sf.spline = connections[1].spline;
            sf.SetPercent(sf.spline.GetPointPercent(temp)+0.001d);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
