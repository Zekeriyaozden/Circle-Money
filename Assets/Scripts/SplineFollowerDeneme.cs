using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SplineFollowerDeneme : MonoBehaviour
{
    private SplineFollower sf;
    private bool instantFlag;
    public bool isHiring;
    private bool FirstSplineFlag;
    private GameManager gm;
    public double doubleStart;
    private Animator anim;
    private bool workingFlag;
    private bool idleFlag;
    void Start()
    {
        idleFlag = false;
        workingFlag = true;
        anim = GetComponent<Animator>();
        instantFlag = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        FirstSplineFlag = true;
        isHiring = false;
        sf = GetComponent<SplineFollower>();
        doubleStart = (double)(1.0d / (double)(sf.spline.pointCount - 1));
        //sf.onNode += OnNodePassed;
    }

    private void moveTheSpline(SplineTracer.NodeConnection nodeConnection,int index)
    {
        double nodePercent = (double) nodeConnection.point / (sf.spline.pointCount - 1);
        double followerPercent = sf.UnclipPercent(sf.result.percent);
        float distancePastNode = sf.spline.CalculateLength(nodePercent, followerPercent);
        Node.Connection[] connections = nodeConnection.node.GetConnections();
        sf.spline = connections[index].spline;
        double newNodePercent = (double) connections[index].pointIndex / (connections[index].spline.pointCount - 1);
        double newPercent = connections[index].spline.Travel(newNodePercent, distancePastNode, sf.direction);
        sf.SetPercent(newPercent);
    }
    
    
    


    
    void Update()
    {
        if (sf.follow)
        {
            anim.SetBool("Idle",false);
            anim.SetBool("Walk",true);
        }
        else
        {
            anim.SetBool("Idle",true);
            anim.SetBool("Walk",false);
        }

        if (sf.GetPercent() > 0.99d && idleFlag &&  sf.spline.name != "PathFirst")
        {
            sf.follow = false;
        }
        if (FirstSplineFlag && sf.spline.name == "PathFirst" && !isHiring)
        {
            if (sf.GetPercent() > doubleStart)
            {
                sf.follow = false;
                FirstSplineFlag = false;
            }
        }
        if (instantFlag && isHiring)
        {
            GameObject obj = Instantiate(gm.WorkerPrefabs, transform.parent);
            obj.GetComponent<SplineFollower>().spline = gm.firstSpline;
            gm.hireCor.GetComponent<HireController>().notHiredList.Add(obj);
            sf.follow = true;
            instantFlag = false;
        }
        
    }
}
