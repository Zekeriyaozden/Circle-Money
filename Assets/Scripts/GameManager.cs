using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject car;
    public Material chibiDead;
    public GameObject chibiHitParticle;
    //---------------------------------------
    public List<int> hiredWorker;
    public List<int> WorkingWorker;
    public SplineComputer firstSpline;
    public GameObject Player;
    public GameObject PlayerReferance;
    public GameObject WorkerPrefabs;
    public GameObject hireCor;
    public float lerpSpeed;
    public float bezierTime;
    [HideInInspector]
    public bool isPlayerStopped;
    public int stackSize;
    public int MaxStackSize;
    //----------------------------------------
    public float workerHiredSpeed;
    //----------------------------------------
    public int maxPaintRequire;
    public int maxPieceRequire;
    public GameObject stackItemPiecePref;
    public GameObject stackItemPaintPref;
    public float carAnimSpeed;
    public CharacterJoint ch;
    public GameObject moneyParent;
    
    
    void Start()
    {
        Application.targetFrameRate = 240;
    }

    public IEnumerator moneyEarn()
    {
        yield return new WaitForSecondsRealtime(2.8f);
        int count = moneyParent.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSecondsRealtime(.1f);
            moneyParent.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = true;
            moneyParent.transform.GetChild(i).gameObject.GetComponent<Collider>().isTrigger = true;
            if (i == count - 1)
            {
                StartCoroutine(moneyMotion(moneyParent.transform.GetChild(i).gameObject , true));
            }
            else
            {
                StartCoroutine(moneyMotion(moneyParent.transform.GetChild(i).gameObject));
            }
        }
    }

    private IEnumerator moneyMotion(GameObject money,bool isLastMoney = false)
    {
        float k = 0;
        Vector3 moneyPos = money.transform.position;
        Vector3 moneyScale = money.transform.localScale;
        while (k < 1)
        {
            if (k < 1)
            {
                k += Time.deltaTime*8f;
            }
            else
            {
                k = 1;
            }
            money.transform.localScale = Vector3.Lerp(moneyScale,moneyScale/3f, k);
            money.transform.position = Vector3.Lerp(moneyPos, Player.transform.position + new Vector3(0,0.6f,0), k);
            yield return new WaitForEndOfFrame();
        }
        money.transform.localScale = Vector3.zero;
        if (isLastMoney)
        {
            Debug.Log("asas");
            int count = moneyParent.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Debug.Log("enterHere");
                Destroy(moneyParent.transform.GetChild(i).gameObject);
            }
        }
    }
    
    void Update()
    {
        
    }
}
