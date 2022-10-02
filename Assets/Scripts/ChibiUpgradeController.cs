using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChibiUpgradeController : MonoBehaviour
{
    public GameObject chibies;
    public List<int> chibiSizerMoneyList;
    private int chibiSizerIndex;
    public List<int> chibiSpawnerMoneyList;
    private int chibiSpawnerIndex;
    private GameManager gm;
    public GameObject chibiUI;
    public GameObject buttonSpawner;
    public GameObject buttonSizer;
    private void Start()
    {
        chibiSpawnerIndex = PlayerPrefs.GetInt("ChibiSpawner",0);
        chibiSizerIndex = PlayerPrefs.GetInt("ChibiSizer",0);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void saver()
    {
        PlayerPrefs.SetInt("ChibiSpawner",chibiSpawnerIndex);
        PlayerPrefs.SetInt("ChibiSizer",chibiSizerIndex);
    }

    private void checkButtons()
    {
        bool check = gm.moneyCheck(chibiSpawnerMoneyList[chibiSpawnerIndex]);
        if (check)
        {
            buttonSpawner.GetComponent<Button>().interactable = true;
        }
        else
        {
            buttonSpawner.GetComponent<Button>().interactable = false;
        }
        check = gm.moneyCheck(chibiSizerMoneyList[chibiSizerIndex]);
        if (check)
        {
            buttonSizer.GetComponent<Button>().interactable = true;
        }
        else
        {
            buttonSizer.GetComponent<Button>().interactable = false;
        }
    }

    public void moneySpend(int index)
    {
        if (index == 0)
        {
            bool check = gm.moneyCheck(chibiSpawnerMoneyList[chibiSpawnerIndex]);
            if (check)
            {
                chibies.GetComponent<ChibiController>().chibiSpawner(20);
                gm.updateGameMoney(chibiSpawnerMoneyList[chibiSpawnerIndex]);
                if (chibiSpawnerIndex - 1 < chibiSpawnerMoneyList.Count)
                {
                    chibiSpawnerIndex++;
                }
            }
            else
            {
                
            }
        }else if (index == 1)
        {
            bool check = gm.moneyCheck(chibiSizerMoneyList[chibiSizerIndex]);
            if (check)
            {
                chibies.GetComponent<ChibiController>().chibiSizer();
                gm.updateGameMoney(chibiSizerMoneyList[chibiSizerIndex]);
                if (chibiSizerIndex - 1 < chibiSizerMoneyList.Count)
                {
                    chibiSizerIndex++;
                }
            }
            else
            {
                
            }
        }
    }

    private void Update()
    {
        saver();
        checkButtons();
    }

    public void UIDeactive(bool state)
    {
        chibiUI.SetActive(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIDeactive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIDeactive(false);
        }
    }
}
