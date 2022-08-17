using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SupplierController : MonoBehaviour
{
    private SplineFollower sfl;
    public GameObject storage;
    public Transform stackReferance;
    public bool canStop;
    public List<GameObject> items;
    public List<GameObject> itemsStack;
    public bool isPaint;
    public int stackSize;
    public int suplySize;
    public int suplySizeMax;
    private GameManager gm;
    private bool corFlag;
    private Animator animator;
    public bool isVert;
    void Start()
    {
        corFlag = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        canStop = true;
        sfl = gameObject.GetComponent<SplineFollower>();
        animator = gameObject.GetComponent<Animator>();
    }

    private IEnumerator Stack()
    {
        while (true)
        {
            if (items.Count == 0)
            {
                break;
            }
            yield return new WaitForSeconds(.4f);
            if (items.Count>0)
            {
                if (itemsStack.Count < suplySizeMax)
                {
                    itemsStack.Add(items[items.Count - 1]);
                    items[items.Count - 1].AddComponent<SupplierBezier>();
                    items[items.Count - 1].GetComponent<SupplierBezier>().startPos = items[items.Count - 1].transform.position;
                    items[items.Count - 1].GetComponent<SupplierBezier>().parentObj = storage;
                    float temp = (float) (itemsStack.Count-1);
                    float tempKalan = (int) temp % 5;
                    int tempDevide = (int) (temp / 5);
                    if (isVert)
                    {
                        items[items.Count - 1].GetComponent<SupplierBezier>().targetPos = stackReferance.position +
                            new Vector3(0.5f * tempKalan, 0 , -0.5f * tempDevide);
                    }
                    else
                    {
                        items[items.Count - 1].GetComponent<SupplierBezier>().targetPos = stackReferance.position +
                            new Vector3(0.5f * tempDevide, 0 , 0.5f * tempKalan);
                    }

                    items.RemoveAt(items.Count - 1);
                }
            }
            else
            {
                break;
            }
        }
        canStop = false;
        corFlag = true;
    }
    void Update()
    {
        if (items.Count > 0)
        {
            animator.SetBool("isWBox",true);
        }
        else
        {
            animator.SetBool("isWBox",false);
        }
        if (sfl.GetPercent() > (double) 0.47 && sfl.GetPercent() < (double) 0.53 && canStop)
        {
            sfl.follow = false;
            animator.SetBool("isRunning",false);
            if (corFlag)
            {
                Debug.Log("ent");
                corFlag = false;
                StartCoroutine(Stack());
            }
        }
        else if (sfl.GetPercent() > (double) 0.95 || sfl.GetPercent() < (double) 0.05)
        {
            while (items.Count < stackSize)
            {
                if (isPaint)
                {
                    GameObject temp = Instantiate(gm.stackItemPaintPref);
                    temp.transform.position =
                        gameObject.transform.GetChild(0).transform.position + new Vector3(0, .5f * items.Count, 0);
                    items.Add(temp);
                    temp.transform.SetParent(gameObject.transform);
                }
                else
                {
                    GameObject temp = Instantiate(gm.stackItemPiecePref);
                    temp.transform.position =
                        gameObject.transform.GetChild(0).transform.position + new Vector3(0, .5f * items.Count, 0);
                    items.Add(temp);
                    temp.transform.SetParent(gameObject.transform);
                }
            }
            canStop = true;
        }
        else
        {
            sfl.follow = true;
            animator.SetBool("isRunning",true);
        }
    }
}
