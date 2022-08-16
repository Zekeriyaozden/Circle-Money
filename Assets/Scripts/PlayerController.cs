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
    public bool isStopped;
    public bool isCorStart;
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
        while (0 < go.transform.childCount)
        {
            if (gm.GetComponent<GameManager>().MaxStackSize <= stackList.Count)
            {
                break;
            }
            yield return new WaitForSeconds(.2f);
            if (!isStopped)
            {
                break;
            }
            GameObject temp = go.transform.GetChild(0).gameObject;
            stackList.Add(go.transform.GetChild(0).gameObject);
            go.transform.GetChild(0).parent = gameObject.transform;
            temp.AddComponent<ItemBezier>().startPosDistance = temp.transform.position;
            int TempFlt = (stackList.Count) - 1;
            temp.GetComponent<ItemBezier>().targetPos = gm.GetComponent<GameManager>().PlayerReferance.transform;
            temp.GetComponent<ItemBezier>().count = TempFlt;
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
