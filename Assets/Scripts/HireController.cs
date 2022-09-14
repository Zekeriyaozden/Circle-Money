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
    private bool hireDecrease;
    public GameObject image;
    private Image slide;
    void Start()
    {
        hireDecrease = true;
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
            slide.fillAmount = 0f;
            if (notHiredList.Count > 0 && triggerFlag)
            {
                notHiredList[0].gameObject.GetComponent<SplineFollowerDeneme>().isHiring = true;
                GameObject temp = notHiredList[0];
                notHiredList.Remove(temp);
            }   
        }
    }

    private IEnumerator decreaseUI()
    {
        float k = slide.fillAmount;
        while (k > 0 && hireDecrease)
        {
            yield return new WaitForEndOfFrame();
            k -= Time.deltaTime / 3f;
            slide.fillAmount = k;
        }
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            hireDecrease = false;
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
            hireDecrease = true;
            triggerFlag = false;
            corFlag = true;
            StartCoroutine(decreaseUI());
        }
    }
}
