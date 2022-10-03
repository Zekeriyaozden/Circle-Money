using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using DG.Tweening;
using Dreamteck.Splines;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    [Range(1, 4)] public float Colspeed;
    public GameObject car;
    public Material chibiDead;
    //public GameObject chibiHitParticle;
    //---------------------------------------
    //public List<int> hiredWorker;
    //public List<int> WorkingWorker;
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
    public GameObject moneyParent;
    //----------------ECONOMY------------------
    public int money;
    public GameObject moneyUI;
    public Vector3 moneyUISize;
    public TextMeshProUGUI MoneyUI;
    //----------------TARGET BOOL--------------
    public bool targetFlag1;
    public bool targetFlag2;
    public bool targetFlag3;
    public bool firstCarGet;
    public bool tutorialEnd;
    //-----------------------------------------
    //------------------ChibiCount-------------
    public int chibi;
    public GameObject ChibiCount;
    private GameObject ChibiCountParent;
    private Vector3 chibiUIsize;
    public bool isDrive;
    //-----------------------------------------
    void Start()
    {
        ChibiCountParent = ChibiCount.transform.parent.gameObject;
        chibiUIsize = ChibiCountParent.transform.localScale;
        firstCarGet = true;
        targetFlag1 = true;
        targetFlag2 = false;
        tutorialEnd = false;
        moneyUISize = moneyUI.transform.localScale;
        moneyCount();
        Application.targetFrameRate = 240;
    }

    public bool moneyCheck(int moneySpend)
    {
        if (money < moneySpend)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public void updateGameMoney(int size, bool isIncrease = false)
    {
        if (isIncrease)
        {
            money += size;
        }
        else
        {
            money -= size;
        }
        StartCoroutine(UIsizer());
    }



    public void chibiUI(bool isReset=false)
    {
        if (isReset)
        {
            ChibiCount.GetComponent<TextMeshProUGUI>().text = "0/20";
            chibi = 0;
        }
        else
        {
            if (isDrive)
            {
                chibi++;
                StartCoroutine(chibiUIsizer());   
            }
        }
    }

    private void chibiUIController()
    {
        if (chibi<=20)
        {
            ChibiCount.GetComponent<TextMeshProUGUI>().text = chibi.ToString() + "/20";
        }
        else
        {
            chibi = 20;
        }
    }
    
    private IEnumerator chibiUIsizer()
    {
        float k = 0;
        while (k < 1)
        {
            ChibiCountParent.transform.localScale = Vector3.Lerp(chibiUIsize, chibiUIsize * 1.5f, k);
            k += Time.deltaTime * 8f;
            yield return new WaitForEndOfFrame();
        }
        k = 0;
        Vector3 v3 = ChibiCountParent.transform.localScale;
        chibiUIController();
        while (k < 1)
        {
            ChibiCountParent.transform.localScale = Vector3.Lerp(v3, chibiUIsize, k);
            k += Time.deltaTime * 10f;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator UIsizer()
    {
        float k = 0;
        while (k < 1)
        {
            moneyUI.transform.localScale = Vector3.Lerp(moneyUISize, moneyUISize * 1.5f, k);
            k += Time.deltaTime * 8f;
            yield return new WaitForEndOfFrame();
        }
        k = 0;
        Vector3 v3 = moneyUI.transform.localScale;
        moneyCount();
        while (k < 1)
        {
            moneyUI.transform.localScale = Vector3.Lerp(v3, moneyUISize, k);
            k += Time.deltaTime * 10f;
            yield return new WaitForEndOfFrame();
        }
    }

    private void moneyCount()
    {
        if (money >= 1000)
        {
            MoneyUI.text = (money / 1000).ToString() + "." + ((money - ((money / 1000) * 1000)) / 100).ToString() + "K";
        }
        else
        {
            MoneyUI.text = money.ToString();
        }
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
            int count = moneyParent.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Destroy(moneyParent.transform.GetChild(i).gameObject);
            }
        }
        updateGameMoney(100,true);
    }
    
    void Update()
    {

    }
}
