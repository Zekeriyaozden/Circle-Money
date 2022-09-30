using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    private GameManager gm;
    private GameObject mainPlayer;
    public GameObject targetFirst;
    public GameObject targetSecond;
    public GameObject targetThird;
    public GameObject carImage;
    public bool getInCar;
    public float k;
    public int s;
    public GameObject Canvas;
    public GameObject Cube;
    void Start()
    {
        getInCar = false;
        k = 0;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainPlayer = gm.Player;
        gradiant();
    }
    public void gradiant()
    {
        StartCoroutine(gradiCor());
    }

    private IEnumerator gradiCor()
    {
        bool isIncrease = true;
        while (gm.firstCarGet)
        {
            s =(int) Mathf.Lerp(0, 255f, k);
            carImage.GetComponent<Image>().color = new Color(s/255f,255f/255f,s/255f);
            if (isIncrease)
            {
                k += Time.deltaTime / 2f;   
            }
            else
            {
                k -= Time.deltaTime / 2f;
            }
            if (k > 1)
            {
                isIncrease = false;
            }
            if (k < 0)
            {
                isIncrease = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    
    void Update()
    {
        if (gm.tutorialEnd)
        {
           Canvas.SetActive(false);
           Cube.SetActive(false);
        }
        else
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
                    if (getInCar)
                    {
                        transform.position += new Vector3(0, 2f, 0);
                        transform.LookAt(targetThird.transform.position);
                        Cube.transform.localPosition = new Vector3(0, 0, 4f);
                    }
                    else
                    {
                        Canvas.SetActive(false);
                    }
                }
            }
        }
    }
}
