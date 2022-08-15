using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform mainChar;
    private Vector3 distance;
    void Start()
    {
        distance = transform.position - mainChar.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = distance + mainChar.position;
    }
}
