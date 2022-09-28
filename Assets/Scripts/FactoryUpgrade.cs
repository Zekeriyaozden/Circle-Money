using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryUpgrade : MonoBehaviour
{
    public GameObject canvasFactory;
    private GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cross()
    {
        canvasFactory.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.targetFlag1 = false;
            canvasFactory.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvasFactory.SetActive(false);
        }
    }
    
}
