using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackFactoryController : MonoBehaviour
{
   public List<GameObject> paintList;
   public List<GameObject> pieceList;
   public GameObject referance;
   private bool isStopped;
   private bool isCorStart;
   private GameObject gm;

   private void Start()
   {
      gm = GameObject.Find("GameManager").gameObject;
   }

   private void Update()
   {
      isCorStart = gm.GetComponent<GameManager>().Player.GetComponent<PlayerController>().isCorStart;
      isStopped = gm.GetComponent<GameManager>().isPlayerStopped;
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
            if (pieceList.Count < gm.GetComponent<GameManager>().maxPieceRequire)
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
                              gm.GetComponent<GameManager>().PlayerReferance.gameObject;
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
            
            if (paintList.Count < gm.GetComponent<GameManager>().maxPaintRequire && piece)
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
                              gm.GetComponent<GameManager>().PlayerReferance.gameObject;
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
            gm.GetComponent<GameManager>().Player.GetComponent<PlayerController>().isCorStart = false;
         }
      }
   }
}
