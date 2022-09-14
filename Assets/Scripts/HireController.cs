using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HireController : MonoBehaviour
{
    public List<GameObject> notHiredList;
    private GameManager gm;
    private bool triggerFlag;
    private bool corFlag;
    public GameObject image;
    private Image slide;
    void Start()
    {
        slide = image.GetComponent<Image>();
        corFlag = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator hire()
    {
        while (triggerFlag)
        {
            float k = slide.fillAmount;
            while (k < 1 && triggerFlag)
            {
                yield return new WaitForEndOfFrame();
                k += Time.deltaTime / 5f;
                slide.fillAmount = k;
            }

            if (slide.fillAmount >= 1)
            {
                slide.fillAmount = 0;
                if (notHiredList.Count > 0 && triggerFlag)
                {
                    notHiredList[0].gameObject.GetComponent<SplineFollowerDeneme>().isHiring = true;
                    GameObject temp = notHiredList[0];
                    notHiredList.Remove(temp);
                }   
            }
            else
            {
                
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerFlag = true;
            if (corFlag)
            {
                StartCoroutine(hire());
                corFlag = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerFlag = false;
            corFlag = true;
        }
    }
}
