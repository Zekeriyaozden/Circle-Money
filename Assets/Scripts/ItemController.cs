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
            if (node.gameObject.tag == "Ref")
            {
                transform.position = node.transform.position + new Vector3(0,0.5f,0);
                gameObject.transform.eulerAngles = node.transform.eulerAngles;
            }
            else
            {
                gameObject.transform.position =
                    new Vector3(
                        Mathf.Lerp(transform.position.x,node.transform.position.x,Time.unscaledDeltaTime * lerpSpeed),
                        node.transform.position.y,
                        Mathf.Lerp(transform.position.z,node.transform.position.z,Time.unscaledDeltaTime * lerpSpeed)) +
                    new Vector3(0, 0.5f, 0);
                gameObject.transform.eulerAngles = node.transform.eulerAngles;
            }

        }
        
    }
}
