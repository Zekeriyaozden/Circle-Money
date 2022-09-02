using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class CostumerController : MonoBehaviour
{
    public List<GameObject> customers;
    public GameObject customerPref;
    private bool flagCar;
    private bool carEnumFlag;
    private void Start()
    {
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
        float k = 0;
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
        if (carEnumFlag)
        {
            GameObject cs = customers[0];
            customers.RemoveAt(0);  
            Destroy(cs);
            StartCoroutine(customerTarget());
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
                StartCoroutine(car());
                flagCar = false;
            }
        }
    }
}
