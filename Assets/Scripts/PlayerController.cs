using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private SplineFollower sf;
    public float speed;
    public DynamicJoystick variableJoystick;
    public Vector3 direction;
    public float followSpeed;
    public List<GameObject> stackList;
    public GameObject gm;
    public bool isStopped;
    public bool isCorStart;
    public bool isCorStartHire;
    public GameObject collectedParent;
    private Animator animator;
    public float _dirTemp;
    void Start()
    {
       // fsd = (float) sf.GetPercent();
        animator = gameObject.GetComponent<Animator>();
        isCorStart = true;
        isCorStartHire = true;
        gm = GameObject.Find("GameManager");
        //sf = GetComponent<SplineFollower>();
        //sf.followSpeed = followSpeed;
        animator.SetBool("isWBox",true);
        animator.SetBool("isRunning",false);
    }

    private void FixedUpdate()
    {
        
        direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        Vector3 dir = gameObject.transform.position + direction;
        _dirTemp = direction.magnitude;
        if (direction.magnitude > 0.2f)
        {
            gameObject.transform.DOLookAt(dir, .3f);
            transform.Translate(Vector3.forward * speed * direction.magnitude,Space.Self);
            animator.speed = Mathf.Clamp(direction.magnitude, 0.6f, 1f);
            gm.GetComponent<GameManager>().isPlayerStopped = false;
            isStopped = false;
            animator.SetBool("isRunning",true);
            StopAllCoroutines();
            isCorStart = true;
            isCorStartHire = true;
        }
        else
        {
            gm.GetComponent<GameManager>().isPlayerStopped = true;
            animator.SetBool("isRunning",false);
            isStopped = true;
        }
    }

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
            int tempInt = go.transform.childCount - 1;
            GameObject temp = go.transform.GetChild(tempInt).gameObject;
            supplierList.GetComponent<SupplierController>().itemsStack.Remove(temp);
            go.transform.GetChild(tempInt).parent = collectedParent.transform;
            temp.AddComponent<ItemBezier>().startPosDistance = temp.transform.position;
            int TempFlt = (stackList.Count);
            temp.GetComponent<ItemBezier>().targetPos = gm.GetComponent<GameManager>().PlayerReferance.transform;
            temp.GetComponent<ItemBezier>().count = TempFlt;
            temp.GetComponent<ItemBezier>().pc = gameObject.GetComponent<PlayerController>();
            //stackList.Add(go.transform.GetChild(tempInt).gameObject);
            gm.GetComponent<GameManager>().stackSize = stackList.Count;
        }
    } 
    //----------------------HIRE--------------------------------//
    private IEnumerator hireWorkerCor(GameObject obj)
    {
        Debug.Log("Enter");
        while (true)
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
        
            if (obj.transform.childCount > 1)
            {
                GameObject worker = obj.transform.GetChild(obj.transform.childCount - 1).gameObject;   
                worker.transform.SetParent(null);
                worker.AddComponent<HiredWorkerController>().targetTransform = gameObject.transform;
                

            }
        }
    }
    //----------------------------------------------------------//

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

        if (other.tag == "Hire")
        {
            if (isStopped && isCorStartHire)
            {
                StartCoroutine(hireWorkerCor(other.gameObject));
                isCorStartHire = false;
            }
        }
    }
}
