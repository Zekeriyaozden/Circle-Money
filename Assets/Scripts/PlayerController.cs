using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SplineFollower sf;
    public float followSpeed;
    public List<GameObject> stackList;
    public GameObject gm;
    private bool isStopped;
    private bool isCorStart;
    void Start()
    {
        isCorStart = true;
        gm = GameObject.Find("GameManager");
        sf = GetComponent<SplineFollower>();
        sf.followSpeed = followSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        sf.followSpeed = followSpeed;
        
        if (Input.GetMouseButton(0))
        {
            gm.GetComponent<GameManager>().isPlayerStopped = false;
            sf.follow = true;
            isStopped = false;
            StopAllCoroutines();
            isCorStart = true;
        }
        else
        {
            gm.GetComponent<GameManager>().isPlayerStopped = true;
            sf.follow = false;
            isStopped = true;
        }
    }

    private IEnumerator stackEffect(GameObject go)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            yield return new WaitForSeconds(.2f);
            if (!isStopped)
            {
                break;
            }
            go.transform.GetChild(i).parent = gameObject.transform;
            stackList.Add(go);
            //go.transform.localPosition = 
        }
    } 

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "StackController")
        {
            if (isStopped && isCorStart)
            {
                StartCoroutine(stackEffect(other.gameObject.GetComponent<StackController>().stackController));
                isCorStart = false;
            }
        }
    }
    
}
