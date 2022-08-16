
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFactoryBezier : MonoBehaviour
{
    private GameObject gm;
    private float time;
    public Vector3 startPos;
    public Vector3 targetPos;
    private Vector3 startBezPos;
    private Vector3 targetBezPos;
    private Vector3 secondPosDistance;
    private float k;

    void Start()
    {
        gm = GameObject.Find("GameManager");
        time = gm.GetComponent<GameManager>().bezierTime;
        k = 0;
    }

    
    void Update()
    {
        secondPosDistance = Vector3.Lerp(startPos, targetPos, 0.5f);
        secondPosDistance += new Vector3(0, 1.2f, 0);
        if (k < 1)
        {
            k += Time.deltaTime / time;
        }
        else
        {
            k = 1;
        }
        startBezPos = Vector3.Lerp(startPos, secondPosDistance, k);
        targetBezPos = Vector3.Lerp(secondPosDistance,targetPos, k);
        gameObject.transform.position = Vector3.Lerp(startBezPos, targetBezPos, k);
        

        if (k == 1)
        {
            gameObject.GetComponent<ItemBezier>().enabled = false;
        }


    }
}
