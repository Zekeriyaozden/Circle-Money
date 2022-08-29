using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireController : MonoBehaviour
{
    public List<GameObject> notHiredList;
    private GameManager gm;
    private bool triggerFlag;
    private bool corFlag;
    // Start is called before the first frame update
    void Start()
    {
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
            yield return new WaitForSecondsRealtime(4f);
            if (notHiredList.Count > 0 && triggerFlag)
            {
                notHiredList[0].gameObject.GetComponent<SplineFollowerDeneme>().isHiring = true;
                GameObject temp = notHiredList[0];
                notHiredList.Remove(temp);
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
        triggerFlag = false;
        corFlag = true;
    }
}
