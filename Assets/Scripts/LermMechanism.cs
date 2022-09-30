/*using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LermMechanism : MonoBehaviour
{
    private GameManager gm;
    public PlayerController playerControl;
    private float lerpSpeed;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        lerpSpeed = gm.lerpSpeed;
    }

    void FixedUpdate()
    {
        int cnt = playerControl.stackList.Count;
        if (cnt > 0)
        {
            for (int i = 0; i < cnt; i++)
            {
                GameObject obj = playerControl.stackList[i].gameObject;
                Transform nodePos = obj.gameObject.GetComponent<ItemController>().node.gameObject.transform;
                obj.transform.DOMove(nodePos.position + new Vector3(0,0.5f,0),lerpSpeed);
                //obj.transform.eulerAngles = node.transform.eulerAngles;
                /*    
                Vector3 pos = transform.position;
                pos.y = node.transform.position.y + 0.5f;
                pos.x = node.transform.position.x;
                pos.z = node.transform.position.z;
                transform.DOMove(pos, lerpSpeed);
                gameObject.transform.eulerAngles = node.transform.eulerAngles;
            }
        }
    }
}
*/