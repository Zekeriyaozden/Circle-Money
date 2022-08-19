using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class CostumerController : MonoBehaviour
{
    public List<GameObject> costumerList;
    public List<GameObject> nodeList;
    public GameObject costumerPrefabs;
    public GameObject costumerPath;
    void Start()
    {
        
    }

    IEnumerator objControl(float start, float finish,GameObject obj,bool turnControl=false)
    {
        SplinePositioner sP = obj.GetComponent<SplinePositioner>();
        sP.spline = gameObject.GetComponent<SplineComputer>();
        float k = 0;
        Animator anim = obj.GetComponent<Animator>();
        anim.SetBool("Walk",true);
        anim.SetBool("Idle",false);
        if (turnControl == false)
        {
            Debug.Log(sP.GetPercent());
        }
        for (int i = 0; i < 80; i++)
        {
            k += i * 0.2f / 80f;
            yield return new WaitForSeconds(.6f / 80f);
            double percent = (double) Mathf.Lerp(start, finish, k);
            sP.SetPercent(percent);
        }
        anim.SetBool("Walk",false);
        anim.SetBool("Idle",true);

        if (turnControl)
        {
            if (costumerList.IndexOf(obj) < costumerList.Count - 1)
            {
                StartCoroutine(objControl(start - 0.2f, start, costumerList[costumerList.IndexOf(obj) + 1], true));
            }
            else
            {
                GameObject temp = costumerList[0];
                costumerList.Remove(temp);
                GameObject gObjIns = Instantiate(costumerPrefabs, transform);
                gObjIns.GetComponent<SplinePositioner>().spline = gameObject.GetComponent<SplineComputer>();
                costumerList.Add(gObjIns);
                StartCoroutine(objControl(0, 0.2f, gObjIns, false));
            }
        }
    }

    public void turnControl()
    {
        StartCoroutine(objControl(.8f, 1f, costumerList[1],true));
    }
    void Update()
    {

        
    }
}
