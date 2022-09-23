using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FactoryController : MonoBehaviour
{
    public int indexOfFactory;
    public float lerpSpeed;
    public GameObject carReferance;
    public GameObject carParent;
    public GameObject carTarget;
    private GameObject AfterMaking;
    private bool MakeCarFlag;
    private GameManager gm;
    private GameObject CurrentCar;
    public List<GameObject> carList;
    public Image slide;
    void Start()
    {
        AfterMaking = null;
        MakeCarFlag = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void makeCar(int index)
    {
        int rand = Random.Range(0, carList.Count);
        GameObject obj = Instantiate(carList[index], carReferance.transform.position, carReferance.transform.rotation,carParent.transform);
        obj.GetComponent<Animator>().speed *= 3f; 
        CurrentCar = obj;
        StartCoroutine(tempFlagControl());
    }

    private void AfterMakeCar()
    {
        Vector3 middle = carTarget.transform.position + (Vector3.up * 4);
        StartCoroutine(bezier(CurrentCar.transform.position, carTarget.transform.position, middle, CurrentCar));
        AfterMaking = CurrentCar;
        CurrentCar = null;
    }

    private IEnumerator bezier(Vector3 startPos,Vector3 target,Vector3 Middle,GameObject obj)
    {
        Vector3 startToMiddle;
        Vector3 middleToTarget;
        float k = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (k < 1)
            {
                k += Time.deltaTime * lerpSpeed;
            }
            else
            {
                k = 1;
            }
            startToMiddle = Vector3.Lerp(startPos,Middle,k);
            middleToTarget = Vector3.Lerp(Middle, target, k);
            obj.transform.position = Vector3.Lerp(startToMiddle, middleToTarget, k);
            if (k == 1)
            {
                break;
            }
        }
        obj.GetComponent<CarController>().isCarReady = true;
        MakeCarFlag = true;
    }
    

    private IEnumerator tempFlagControl()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (CurrentCar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CarAnim"))
            {
                continue;
            }
            else
            {
                if (AfterMaking == null)
                {
                    AfterMakeCar();
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void MakeCarUI()
    {
        if (MakeCarFlag)
        {
            makeCar(0);
            MakeCarFlag = false;
        }
    }
    
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            MakeCarUI();
        }
        
        if (AfterMaking != null)
        {
            if (AfterMaking.GetComponent<CarController>().ridingCar)
            {
                AfterMaking.transform.parent = null;
                AfterMaking = null;
            }
        }
    }
    


    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
