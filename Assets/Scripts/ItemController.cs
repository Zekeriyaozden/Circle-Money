using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string itemType;
    public GameObject node;
    public bool collected;
    private float lerpSpeed;

    void Start()
    {
        lerpSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().lerpSpeed;
    }

    // Update is called once per frame
    // gameObject.transform.position = new Vector3((Mathf.Lerp( gameObject.transform.position.x,node.gameObject.transform.position.x ,Time.deltaTime * lerpSpeed)), parent.transform.position.y + distanceY ,parent.transform.position.z);
    void Update()
    {
       /*if (collected)
        {

                Vector3 pos = transform.position;
                pos.y = node.transform.position.y + 0.5f;
                pos.x = node.transform.position.x;
                pos.z = node.transform.position.z;
                transform.DOMove(pos, lerpSpeed);
                gameObject.transform.eulerAngles = node.transform.eulerAngles;
         
        }*/
    }
}
