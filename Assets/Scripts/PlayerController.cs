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
    public GameObject collectedParent;
    private Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        isCorStart = true;
        gm = GameObject.Find("GameManager");
        sf = GetComponent<SplineFollower>();
        sf.followSpeed = followSpeed;
        animator.SetBool("isWBox",true);
        animator.SetBool("isRunning",false);
    }

    // Update is called once per frame
    void Update()
    {
        if (stackList.Count > 0)
        {
            animator.SetBool("isWBox",true);
        }
        else
        {
            animator.SetBool("isWBox",false);
        }
        
        
        sf.followSpeed = followSpeed;
        
        if (Input.GetMouseButton(0))
        {
            gm.GetComponent<GameManager>().isPlayerStopped = false;
            sf.follow = true;
            isStopped = false;
            animator.SetBool("isRunning",true);
            StopAllCoroutines();
            isCorStart = true;
        }
        else
        {
            gm.GetComponent<GameManager>().isPlayerStopped = true;
            sf.follow = false;
            animator.SetBool("isRunning",false);
            isStopped = true;
        }
    }

    private IEnumerator stackEffect(GameObject go,GameObject supplierList)
    {
        while (0 < go.transform.childCount)
        {
            if (gm.GetComponent<GameManager>().MaxStackSize <= stackList.Count)
            {
                break;
            }
            yield return new WaitForSeconds(.4f);
            if (!isStopped)
            {
                break;
            }

            //Debug.Log("VAR");
            int tempInt = go.transform.childCount - 1;
            GameObject temp = go.transform.GetChild(tempInt).gameObject;
            supplierList.GetComponent<SupplierController>().itemsStack.Remove(temp);
            stackList.Add(go.transform.GetChild(tempInt).gameObject);
            go.transform.GetChild(tempInt).parent = collectedParent.transform;
            temp.AddComponent<ItemBezier>().startPosDistance = temp.transform.position;
            int TempFlt = (stackList.Count);
            //Debug.Log(TempFlt);
            temp.GetComponent<ItemBezier>().targetPos = gm.GetComponent<GameManager>().PlayerReferance.transform;
            temp.GetComponent<ItemBezier>().count = TempFlt;
            gm.GetComponent<GameManager>().stackSize = stackList.Count;
        }
    } 

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "StackController")
        {
            if (isStopped && isCorStart)
            {
                StartCoroutine(stackEffect(other.gameObject.GetComponent<StackController>().stackController,other.gameObject.GetComponent<StackController>().supplierController));
                isCorStart = false;
            }
        }
    }
    
}
