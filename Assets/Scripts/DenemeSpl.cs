using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using Random = UnityEngine.Random;

public class DenemeSpl : MonoBehaviour
{
    private bool percent;
    private SplineFollower sf;
    public double db;
    public bool isInStart;
    public string color;
    private bool inSpline;
    private Rigidbody rb;
    private Rigidbody[] rgbs;
    private Collider[] cldrs;
    public GameObject doll;
    private GameObject car;
    private GameManager gm;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //setRagdoll(false);
        inSpline = true;
        percent = true;
        float perc;
        sf = GetComponent<SplineFollower>();
        perc = Random.Range(0f, 1f);
        db = (double)perc;
        StartCoroutine(starts());
    }

    IEnumerator starts()
    {
        float percs = Random.Range(0f, .1f);
        yield return new WaitForSeconds(percs);
        gameObject.GetComponent<Animator>().enabled = true;
    }

    private void setRagdoll(bool stat)
    {
        rgbs = GetComponentsInChildren<Rigidbody>();
        cldrs = GetComponentsInChildren<Collider>();
        foreach (var rgb in rgbs)
        {
            rgb.isKinematic = !stat;
            rgb.mass = rgb.mass / 10f;
        }
        foreach (var cld in cldrs)
        {
            cld.enabled = stat;
        }
        gameObject.GetComponent<Collider>().enabled = true;
        hitCar();
    }

    private void hitCar()
    {
        if (gm.car != null)
        {
            float k = 0;
            car = gm.car;
            k += Time.deltaTime;
            doll.GetComponent<Rigidbody>().AddForce((doll.gameObject.transform.position - car.transform.position + new Vector3(0,1f,0)) * (1200f / 0.32f) * (gm.Player.gameObject.GetComponent<PlayerController>().speedOfCar) , ForceMode.Force);
            Debug.Log("hit");
        }
        else
        {
            Debug.Log("STT");
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        if (inSpline)
        {
            if (percent && isInStart)
            {
                sf.SetPercent(db);      
                percent = false;
            }
            else
            {
                db = sf.GetPercent();
            }
        }
        else
        {
            GetComponent<Animator>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            Debug.Log("EnterCarFill");
            
            if (sf.enabled)
            {
                other.gameObject.GetComponent<CarController>().fillTheCar(color);
                sf.spline = null;
                sf.enabled = false;
                setRagdoll(true);
            }
            GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            inSpline = false;
        }
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Car")
        {
            if (sf.enabled)
            {
                sf.spline = null;
                sf.enabled = false;
                GetComponent<Animator>().enabled = false;
            }
            setRagdoll(true);
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            gameObject.GetComponent<Collider>().enabled = true;
            inSpline = false;
        }
    }*/
}