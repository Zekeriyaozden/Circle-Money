using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class StackFactoryController : MonoBehaviour
{
   public Material mt;
   public List<GameObject> paintList;
   public List<GameObject> pieceList;
   public List<GameObject> workers;
   public GameObject referance;
   public GameObject carReferance;
   public GameObject carPrefab;
   public GameObject CurrentCar;
   private bool isStopped;
   private bool isCorStart;
   private bool tempFlag;
   private GameManager gm;

   private void Start()
   {
      CurrentCar = null;
      gm = GameObject.Find("GameManager").GetComponent<GameManager>();
      tempFlag = true;
   }

   private void Update()
   {
      if (gm.maxPaintRequire <= paintList.Count)
      {
         if (gm.maxPieceRequire <= pieceList.Count)
         {
            if (tempFlag)
            {
               for (int i = 0; i < workers.Count; i++)
               {
                  workers[i].GetComponent<Animator>().SetBool("WorkingCar",true);
                  workers[i].GetComponent<Animator>().SetBool("WorkingPaint",false);
                  workers[i].GetComponent<Animator>().SetBool("Idle",false);
               }
               Material mtr = Instantiate(mt);
               mtr.SetFloat("_Ring",1f);
               GameObject gObj = Instantiate(carPrefab, transform.parent);
               Material[] mtls = gObj.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials;
               mtls[1] = mtr;
               gObj.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials = mtls;
               gObj.transform.position = carReferance.transform.position;
               gObj.transform.eulerAngles = new Vector3(0, 270f, 0);
               gObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
               CurrentCar = gObj;
               StartCoroutine(tempFlagControl(mtr));
               tempFlag = false;
            }
         }
      }
      isCorStart = gm.Player.GetComponent<PlayerController>().isCorStart;
      isStopped = gm.isPlayerStopped;
   }

   private IEnumerator paintToCar(Material mt)
   {
      yield return new WaitForSeconds(.8f);
      for (int i = 0; i < workers.Count; i++)
      {
         workers[i].GetComponent<Animator>().SetBool("WorkingCar",false);
         workers[i].GetComponent<Animator>().SetBool("WorkingPaint",true);
         workers[i].GetComponent<Animator>().SetBool("Idle",false);
      }  
      float k = 0;
      for (int i = 1; i < 200; i++)
      {
         k = (float)i / 200;
         yield return new WaitForSeconds(4f / 200f);
         mt.SetFloat("_Ring",1+( k * (2.35f)));
      }
      for (int i = 0; i < workers.Count; i++)
      {
         workers[i].GetComponent<Animator>().SetBool("WorkingCar",false);
         workers[i].GetComponent<Animator>().SetBool("WorkingPaint",false);
         workers[i].GetComponent<Animator>().SetBool("Idle",true);
      }
   }
   

   private IEnumerator tempFlagControl(Material mt)
   {
      while (true)
      {
         yield return new WaitForSeconds(.1f);
         if (CurrentCar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CarAnim"))
         {
            continue;
         }
         else
         {
            StartCoroutine(paintToCar(mt));
            break;
         }
      }
   }

   private IEnumerator stackEffect(GameObject player)
   {
      PlayerController pc = player.GetComponent<PlayerController>();
      while (true)
      {
         if (!isStopped)
         {
            break;
         }
         if (pc.stackList.Count > 0)
         {
            bool piece = true;
            if (pieceList.Count < gm.maxPieceRequire)
            {
               for (int i = pc.stackList.Count-1; i >= 0 ; i--)
               {
                  Debug.Log(i + "--" + pc.stackList.Count);
                  if (pc.stackList[i].GetComponent<ItemController>().itemType == "Piece" && piece)
                  {
                     pieceList.Add(pc.stackList[i]);
                     pc.stackList[i].GetComponent<ItemController>().collected = false;
                     int tempIndex = pc.stackList.IndexOf(pc.stackList[i]);
                     if (tempIndex == 0)
                     {
                        if (pc.stackList.Count > 1)
                        {
                           pc.stackList[1].GetComponent<ItemController>().node =
                              gm.PlayerReferance.gameObject;
                        }

                     }
                     else
                     {
                        if (pc.stackList.Count == tempIndex + 1)
                        {
                           
                        }
                        else
                        {
                           pc.stackList[tempIndex + 1].GetComponent<ItemController>().node =
                              pc.stackList[tempIndex - 1];
                        }
                     }
                     GameObject tempObj = pc.stackList[i];
                     pc.stackList.Remove(tempObj);
                     tempObj.AddComponent<StackFactoryBezier>().startPos = tempObj.transform.position;
                     tempObj.GetComponent<StackFactoryBezier>().targetPos =
                        referance.transform.position;
                     //i = pc.stackList.Count + 20;
                     
                     piece = false;
                  }
               }
            }
            else
            {
               //todo maxPaint
            }
            
            if (paintList.Count < gm.maxPaintRequire && piece)
            {
               for (int i = pc.stackList.Count-1; i >= 0 ; i--)
               {
                  if (pc.stackList[i].GetComponent<ItemController>().itemType == "Paint" && piece)
                  {
                     paintList.Add(pc.stackList[i]);
                     pc.stackList[i].GetComponent<ItemController>().collected = false;
                     int tempIndex = pc.stackList.IndexOf(pc.stackList[i]);
                     if (tempIndex == 0)
                     {
                        if (pc.stackList.Count > 1)
                        {
                           pc.stackList[1].GetComponent<ItemController>().node =
                              gm.PlayerReferance.gameObject;
                        }
                     }
                     else
                     {
                        if (pc.stackList.Count == tempIndex + 1)
                        {
                           
                        }
                        else
                        {
                           pc.stackList[tempIndex + 1].GetComponent<ItemController>().node =
                              pc.stackList[tempIndex - 1];
                        }
                     }
                     GameObject tempObj = pc.stackList[i];
                     pc.stackList.Remove(tempObj);
                     tempObj.AddComponent<StackFactoryBezier>().startPos = tempObj.transform.position;
                     tempObj.GetComponent<StackFactoryBezier>().targetPos =
                        referance.transform.position;
                     //i = pc.stackList.Count + 20;
                     piece = false;
                  }
               }
            }
            else
            {
               //todo maxPaint
            }
         }
         yield return new WaitForSeconds(.4f);
         if (!isStopped)
         {
            break;
         }
      }
   }

   private void OnTriggerStay(Collider other)
   {
      if (other.tag == "Player")
      {
         if (isStopped && isCorStart)
         {
            Debug.Log("entered");
            StartCoroutine(stackEffect(other.gameObject));
            isCorStart = false;
            gm.Player.GetComponent<PlayerController>().isCorStart = false;
         }
      }
   }
}
