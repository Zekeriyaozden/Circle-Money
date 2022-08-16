using System.Collections;
using System.Collections.Generic;
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
        if (collected)
        {
            gameObject.transform.position =
                new Vector3((Mathf.Lerp(gameObject.transform.position.x, node.transform.position.x, Time.deltaTime * lerpSpeed)),
                    (Mathf.Lerp(gameObject.transform.position.y, node.transform.position.y + 0.31f, Time.deltaTime * lerpSpeed)),
                    (Mathf.Lerp(gameObject.transform.position.z, node.transform.position.z, Time.deltaTime * lerpSpeed)));
        }
        
    }
}
