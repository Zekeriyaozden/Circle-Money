using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float animDuration;
    public GameObject playerGetOutTarget;
    public bool ridingCar;
    public bool isCarReady;
    public GameObject readyObject;
    public GameObject[] wheels;
    public float speedOfWheels;
    private Color clr;
    private Material mt;
    public bool delivered;
    public GameObject Main;
    public GameObject tires;
    private bool tiresFlag;
    void Start()
    {
        tiresFlag = false;
        speedOfWheels = 0;
        delivered = false;
        //mt = transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().materials[0];
        ridingCar = false;
        tiresSettrue();
    }

    public void tiresSettrue()
    {

        foreach (var wheel in wheels)
        {
            Debug.Log("EnterHere-Wheel");
            wheel.SetActive(false);
        }
        int s = tires.transform.childCount;
        for (int i = 0; i < s; i++)
        {
            Debug.Log("EnterHere-Tires");
            tires.transform.GetChild(i).gameObject.GetComponent<Animator>().speed = 0;
        }
        tiresFlag = true;
    }

    void Update()
    {
        if (tiresFlag)
        {
            if (ridingCar)
            {
                int s = tires.transform.childCount;
                for (int i = 0; i < s; i++)
                {
                    Debug.Log("enterRidingCar");
                    tires.transform.GetChild(i).gameObject.GetComponent<Animator>().speed = speedOfWheels;
                }
            }
        }
        if (isCarReady)
        {
            readyObject.SetActive(true);
        }
    }

    /*public void fillTheCar(string ind)
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
    }*/
}
