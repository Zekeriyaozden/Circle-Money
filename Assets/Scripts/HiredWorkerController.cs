using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiredWorkerController : MonoBehaviour
{
    public Vector3 targetOffset;
    public Transform targetTransform;
    private Vector3 target;
    private float speed;
    private GameManager gm;
    
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = gm.workerHiredSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        target = targetTransform.TransformPoint(new Vector3(2, 0, 0));
        if (Vector3.Distance(gameObject.transform.position, target) > .3f)
        {
            gameObject.transform.LookAt(target);
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime,Space.Self);
        }
        else
        {
            //gameObject.transform.Rotate(targetTransform.eulerAngles);
        }
    }
}
