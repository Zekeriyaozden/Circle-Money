using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private GameManager gm;
    private GameObject mainPlayer;
    public GameObject targetFirst;
    public GameObject targetSecond;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainPlayer = gm.Player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainPlayer.transform.position;
        if (gm.targetFlag1)
        {
            transform.LookAt(targetFirst.transform.position);   
        }
        else
        {
            if (gm.targetFlag2)
            {
                transform.LookAt(targetSecond.transform.position);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
