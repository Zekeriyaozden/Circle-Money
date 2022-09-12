using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGetIn : MonoBehaviour
{
    public GameObject image;
    private Image slide;
    private bool UIFlag;
    void Start()
    {
        transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x,0.5f, transform.GetChild(0).localPosition.z);
        slide = image.GetComponent<Image>();
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
            if (k < 1f)
            {
                k += Time.deltaTime / 2f;
            }
            else
            {
                break;
            }
            slide.fillAmount = k;
        }

        if (!UIFlag)
        {
            Debug.Log("Break");
            player.GetComponent<PlayerController>().getCarIn(gameObject);
            transform.parent.parent.gameObject.GetComponent<CarController>().Main = player.gameObject;
            gameObject.SetActive(false);
        }
        else
        {
            slide.fillAmount = 0;
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
