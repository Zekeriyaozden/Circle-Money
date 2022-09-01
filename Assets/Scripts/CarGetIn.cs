using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGetIn : MonoBehaviour
{
    private bool UIFlag;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0.001f, transform.position.z);
        UIFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UIGetIn(GameObject player)
    {
        float k = 0;
        while (!UIFlag)
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("EndOfFrame");
            if (k < 1f)
            {
                k += Time.deltaTime / 2f;
            }
            else
            {
                break;
            }
        }

        if (!UIFlag)
        {
            Debug.Log("Break");
            player.GetComponent<PlayerController>().getCarIn(gameObject);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (UIFlag)
            {
                UIFlag = false;
                StartCoroutine(UIGetIn(other.gameObject));
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UIFlag = true;
        }
    }
}
