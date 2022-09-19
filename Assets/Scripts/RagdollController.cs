using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    public GameObject doll;
    public GameObject car;
    private GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        car = gm.car;
        setRagdoll(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void setRagdoll(bool stat)
    {
        Rigidbody[] rgbs = GetComponentsInChildren<Rigidbody>();
        Collider[] cldrs = GetComponentsInChildren<Collider>();
        foreach (var rgb in rgbs)
        {
            rgb.isKinematic = !stat;
            rgb.mass = rgb.mass / 2f;
        }
        foreach (var cld in cldrs)
        {
            cld.enabled = stat;
        }
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private IEnumerator force()
    {
        float k = 0;
        car = gm.car;
        while (k<0.1f)
        {
            k += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            doll.GetComponent<Rigidbody>().AddForce((doll.gameObject.transform.position - car.transform.position + new Vector3(0,1.5f,0)) * 2f , ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            setRagdoll(true);
            StartCoroutine(force());
        }
    }
    

    private void OnCollisionEnter(Collision other)
    {

    }
}
