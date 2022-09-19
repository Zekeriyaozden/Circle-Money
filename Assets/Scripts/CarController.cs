using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject playerGetOutTarget;
    public bool ridingCar;
    public bool isCarReady;
    public GameObject readyObject;
    public float numberOfChibi;
    public float numberOfChibiRed;
    public float numberOfChibiGreen;
    public float numberOfChibiBlue;
    private Color clr;
    private Material mt;
    public bool delivered;
    public GameObject Main;
    void Start()
    {
        delivered = false;
        mt = transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().materials[0];
        numberOfChibi = numberOfChibiBlue = numberOfChibiGreen = numberOfChibiRed = 0f;
        ridingCar = false;
    }


    void Update()
    {
        if (isCarReady)
        {
            readyObject.SetActive(true);
        }
    }

    public void fillTheCar(string ind)
    {
        if (!delivered)
        {
            if (numberOfChibiBlue + numberOfChibiGreen + numberOfChibiRed > 45)
            {
                numberOfChibi = 3;
                numberOfChibiBlue = numberOfChibiGreen = numberOfChibiRed = 1;
            }
            numberOfChibi++;
            if (ind == "Red")
            {
                numberOfChibiRed++;
            }else if (ind == "Green")
            {
                numberOfChibiGreen++;
            }
            else if(ind == "Blue")
            {
                numberOfChibiBlue++;
            }

            clr = new Color(numberOfChibiRed/numberOfChibi,numberOfChibiGreen/numberOfChibi,numberOfChibiBlue/numberOfChibi);
            mt.color = clr;
            Material[] materials = gameObject.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().materials;
            materials[0] = mt;
            gameObject.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().materials = materials;   
        }
    }
}
