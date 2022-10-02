using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarBuy : MonoBehaviour
{
    public int[] priceOfCars;
    public GameObject[] priceOfCarsText;
    public GameObject[] priceOfCarsButton;
    public GameObject factory;
    private GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void TextUpdater()
    {
        for (int i = 1; i < priceOfCarsText.Length;i++)
        {
            if (priceOfCars[i] >= 1000)
            {
                priceOfCarsText[i].GetComponent<TextMeshProUGUI>().text = (priceOfCars[i] / 1000).ToString() + "." + ((priceOfCars[i] - ((priceOfCars[i] / 1000) * 1000)) / 100).ToString() + "K" + "$";
            }
            else
            {
                priceOfCarsText[i].GetComponent<TextMeshProUGUI>().text = priceOfCars[i].ToString() + "$";
            }
        }
    }

    public void buttonUpdater()
    {
        for (int i = 0; i < priceOfCarsButton.Length; i++)
        {
            bool check = gm.moneyCheck(priceOfCars[i]);
            if (check)
            {
                priceOfCarsButton[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                priceOfCarsButton[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    
    

    public void spendMoneyForCar(int index)
    {
        bool check = gm.moneyCheck(priceOfCars[index]);
        if (check)
        {
            factory.GetComponent<FactoryController>().MakeCarUI(index);
            gm.updateGameMoney(priceOfCars[index]);
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        buttonUpdater();
        TextUpdater();
    }
}
