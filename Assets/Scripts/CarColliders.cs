using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColliders : MonoBehaviour
{
    public Vector3 direction;
    private float speed;
    private GameManager gm;
    private float temp;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        temp = gm.Player.GetComponent<PlayerController>().MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        speed = gm.Colspeed;
    }

    private IEnumerator carCollision(GameObject car)
    {

        gm.Player.GetComponent<PlayerController>().MoveSpeed = 0f;
        //float speed = 2.5f;
        float k = 0;
        while (k < 1)
        {
            //speed = Mathf.Lerp(0f, 15f, k);
            k += Time.deltaTime * 5f;
            car.transform.Translate(direction / speed,Space.World);
            yield return new WaitForEndOfFrame();
        }
        gm.Player.GetComponent<PlayerController>().MoveSpeed = temp;
    }
    
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            StartCoroutine(carCollision(other.gameObject));
        }
    }
}
