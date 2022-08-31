using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool ridingCar;
    public bool isCarReady;
    public GameObject readyObject;
    void Start()
    {
        ridingCar = false;
    }


    void Update()
    {
        if (isCarReady)
        {
            readyObject.SetActive(true);
        }
    }
}
