using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBuy : MonoBehaviour
{
    public int[] priceOfCars;
    public GameObject factory;
    private GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        
    }
}
