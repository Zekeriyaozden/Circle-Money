using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiredWorkerController : MonoBehaviour
{

    private GameManager gm;
    
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
