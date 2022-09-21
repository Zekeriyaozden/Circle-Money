using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CostumerController : MonoBehaviour
{
    public SplineComputer splineCarPath;
    private GameManager gm;
    public List<GameObject> customers;
    public GameObject customerPref;
    private bool flagCar;
    private bool carEnumFlag;
    public GameObject moneyPref;
    public GameObject moneyVector;
    public GameObject moneyParent;
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
        moneyVector.transform.position = car.transform.position;
        for (int i = 0; i < 12; i++)
        {
            GameObject money;
            GameObject gObj = moneyVector.transform.GetChild(i).gameObject;
            if (i < 6)
            {
                money = Instantiate(moneyPref, car.transform.position + new Vector3(0,1,-3f + (.6f * i)), Quaternion.Euler(30, 0, 25f));
            }
            else
            {
                money = Instantiate(moneyPref, car.transform.position + new Vector3( .5f,1,-3f + (.6f * (i % 6))), Quaternion.Euler(30, 0, 25f));   
            }
            money.transform.parent = moneyParent.transform;
            StartCoroutine(moneyLayer(money));
            money.GetComponent<Rigidbody>().AddForce(((gObj.transform.position - car.transform.position) /8f +
                                                      new Vector3(0, 16f, 0)) * 28f,ForceMode.Force);
            float xTorq = Random.Range(-1.2f, 1.2f);
            float zTorq = Random.Range(-1.2f, 1.2f);
            money.GetComponent<Rigidbody>().AddTorque(new Vector3(xTorq, 0, zTorq) * 25f);
            //money.GetComponent<Rigidbody>().isKinematic = true;
        }
        StartCoroutine(gm.moneyEarn());
        Debug.Log("Costumer get Car...!");
        carDriveAI(car,cs);
    }

    private IEnumerator moneyLayer(GameObject moneyObj)
    {
        moneyObj.layer = 8;
        yield return new WaitForSecondsRealtime(1f);
        moneyObj.layer = 0;
    }

    private void carDriveAI(GameObject car,GameObject cs)
    {
        cs.transform.SetParent(car.transform);
        SplinePoint[] pnt = splineCarPath.GetPoints();
        splineCarPath.gameObject.transform.GetChild(0).position = car.transform.position;
        splineCarPath.gameObject.transform.GetChild(0).eulerAngles = new Vector3(0,car.transform.eulerAngles.y,0);
        pnt[0].position = car.transform.position;
        pnt[1].position = splineCarPath.gameObject.transform.GetChild(0).GetChild(0).position;
        splineCarPath.SetPoints(pnt);
        SplineFollower sf = car.AddComponent<SplineFollower>();
        sf.spline = splineCarPath;
        sf.followSpeed = 3f;
        gm.car = null;
    }

    private IEnumerator customerTarget()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            yield return new WaitForSeconds(.2f);
            customers[i].GetComponent<CustomerBehavController>().target = 0.9d - (0.2d * i);
        }
        GameObject cus = Instantiate(customerPref, customers[0].transform.parent);
        cus.GetComponent<SplineFollower>().spline = customers[0].GetComponent<SplineFollower>().spline;
        cus.GetComponent<CustomerBehavController>().target = 0.1d;
        cus.transform.eulerAngles = customers[0].transform.eulerAngles;
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
