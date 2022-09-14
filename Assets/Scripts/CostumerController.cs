using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class CostumerController : MonoBehaviour
{
    private GameManager gm;
    public List<GameObject> customers;
    public GameObject customerPref;
    private bool flagCar;
    private bool carEnumFlag;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        flagCar = true;
        carEnumFlag = true;
        for (int i = 0; i < customers.Count; i++)
        {
            customers[i].GetComponent<CustomerBehavController>().target = 0.9d - (0.2d * i);
        }
    }

    private void Update()
    {
        
    }

    private IEnumerator car()
    {
        Debug.Log("car");
        float k = 0;
        GameObject cs = customers[0];
        while (true)
        {
            if (k < 1)
            {
                k += Time.deltaTime / 2f;
            }
            else
            {
                break;
            }
            yield return new WaitForEndOfFrame();
            if (!carEnumFlag)
            {
                break;
            }
        }
        if (carEnumFlag && gm.car != null)
        {
            Debug.Log("carEnumFlag");
            StartCoroutine(customerGetCar(cs));
            customers.RemoveAt(0);  
            //Destroy(cs);
        }
    }

    private IEnumerator customerGetCar(GameObject cs)
    {
        float k = 0;
        GameObject player = gm.Player;
        GameObject car = player.transform.parent.gameObject;
        gm.Player.transform.parent = null;
        player.GetComponent<PlayerController>().inCar = false;
        player.GetComponent<PlayerController>().driveCar = false;
        Vector3 startPos = player.transform.position;
        Vector3 midPos = new Vector3(car.GetComponent<CarController>().playerGetOutTarget.transform.position.x, 6f,
            car.GetComponent<CarController>().playerGetOutTarget.transform.position.z);
        Vector3 targetPos = new Vector3(car.GetComponent<CarController>().playerGetOutTarget.transform.position.x,0,car.GetComponent<CarController>().playerGetOutTarget.transform.position.z);
        Vector3 startToMid;
        Vector3 midToTarget;
        player.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);
        while (true)
        {
            if (k < 1)
            {
                k += Time.deltaTime / 1.2f;
            }
            else
            {
                break;
            }
            startToMid = Vector3.Lerp(startPos,midPos,k);
            midToTarget = Vector3.Lerp(midPos,targetPos,k);
            player.transform.position = Vector3.Lerp(startToMid,midToTarget,k);
            yield return new WaitForEndOfFrame();
        }
        k = 0;
        startPos = cs.transform.position;
        midPos = car.transform.position + (Vector3.up * 6f);
        targetPos = car.transform.position - (Vector3.up);
        cs.GetComponent<SplineFollower>().enabled = false;
        StartCoroutine(customerTarget());
        while (true)
        {
            if (k < 1)
            {
                k += Time.deltaTime / 1.2f;
            }
            else
            {
                break;
            }
            startToMid = Vector3.Lerp(startPos,midPos,k);
            midToTarget = Vector3.Lerp(midPos,targetPos,k);
            cs.transform.position = Vector3.Lerp(startToMid,midToTarget,k);
            yield return new WaitForEndOfFrame();
        }
        car.GetComponent<CarController>().delivered = false;
        cs.transform.localScale = Vector3.zero;
    }

    private IEnumerator carDriveAI(GameObject car,GameObject cs)
    {
        
        float k = 0;
        while (k<1)
        {
            k+=Time.deltaTime / 8f;
            yield return new WaitForEndOfFrame();
            
        }
    }

    private IEnumerator customerTarget()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            yield return new WaitForSeconds(.2f);
            customers[i].GetComponent<CustomerBehavController>().target = 0.9d - (0.2d * i);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Car")
        {
            carEnumFlag = false;
            flagCar = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            if (flagCar)
            {
                carEnumFlag = true;
                Debug.Log("carEnter");
                StartCoroutine(car());
                flagCar = false;
            }
        }
    }
}
