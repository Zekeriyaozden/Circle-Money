using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private SplineFollower sf;
    public float speed;
    public float speedOfCar;
    public DynamicJoystick variableJoystick;
    public Vector3 direction;
    public float followSpeed;
    public bool inCar;
    public bool driveCar;
    public List<GameObject> stackList;
    public GameObject gm;
    public bool isStopped;
    public bool isCorStart;
    public bool isCorStartHire;
    public GameObject collectedParent;
    private Animator animator;
    public float lerpSpeed;
    public float _dirTemp;
    private Vector3 v3Last;
    private bool UICore;
    void Start()
    {
        UICore = true;
        v3Last = new Vector3(0, 0, 0);
        speedOfCar = 0;
        driveCar = false;
        inCar = false;
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
        if (!inCar)
        {
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

        if (driveCar)
        {
            if (direction.magnitude > 0.2f)
            {
                if (speedOfCar < .1)
                {
                    speedOfCar = .12f;
                }
                if (speedOfCar < 0.32f)
                {
                    if (speedOfCar > .24f)
                    {
                        speedOfCar += Time.deltaTime/5f;
                    }
                    else
                    {
                        speedOfCar += Time.deltaTime/50f;
                    }
                }
                dir = transform.parent.position + direction;
                transform.parent.DOLookAt(dir, .224f/speedOfCar);
                transform.parent.Translate(Vector3.forward * speedOfCar * direction.magnitude,Space.Self);
                v3Last = dir;
            }
            else
            {
               
                if (speedOfCar > 0)
                {
                    speedOfCar -= Time.deltaTime/2f;
                }
                else
                {
                    speedOfCar = 0;
                }
                transform.parent.Translate(Vector3.forward * speedOfCar,Space.Self);  
            }
            
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
        while (true)
        {
            Debug.Log("Hired");
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
            }
        }
    }

    //----------------------------------------------------------//
    
    //----------------------Get In The Car----------------------//
    private IEnumerator getInTheCar(GameObject carChild)
    {
        Vector3 target;
        Vector3 middlev3;
        Vector3 main;
        Vector3 mainToMiddle;
        Vector3 middleToTarget;
        main = transform.position;
        target = carChild.transform.position;
        middlev3 = carChild.transform.GetChild(0).position;
        inCar = true;
        float k = 0;
        Debug.Log("enterGetInTheCar");
        Vector3 eulerMain = transform.eulerAngles;
        Vector3 carsEuler = carChild.transform.parent.eulerAngles;
        animator.SetBool("isRunning",false);
        Vector3 startScale = transform.localScale;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (k < 1)
            {
                k += Time.deltaTime * lerpSpeed;
            }

            if (k >= 1)
            {
                break;
            }

            if (eulerMain.y > carsEuler.y)
            {
                transform.eulerAngles = Vector3.Lerp(eulerMain, carsEuler+new Vector3(0,360f,0), k);
            }
            else
            {
                transform.eulerAngles = Vector3.Lerp(eulerMain, carsEuler, k);
            }

            if (k > .8f)
            {
                transform.localScale = Vector3.Lerp(startScale,new Vector3(0,0,0),k);
            }
            mainToMiddle = Vector3.Lerp(main, middlev3, k);
            middleToTarget = Vector3.Lerp(middlev3, target, k);
            transform.position = Vector3.Lerp(mainToMiddle,middleToTarget,k);
        }
        transform.parent = carChild.transform.parent;
        carChild.transform.parent.gameObject.GetComponent<CarController>().ridingCar = true;
        driveCar = true;
    }



    public void getCarIn(GameObject other)
    {
        if (!inCar)
        {
            StartCoroutine(getInTheCar(other.transform.parent.gameObject));
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }


    /*public IEnumerator getOffCar()
    {
        transform.SetParent(null);
        
    }*/
    
    //----------------------------------------------------------//
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.tag == "CarTake")
        {
            if (!inCar)
            {
                //StopCoroutine(CarUI(other.gameObject));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CarTake")
        {

        }
        
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
