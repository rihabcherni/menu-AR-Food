using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject Pizza;
    public GameObject Chicken;
    public GameObject Coffee;
    public GameObject Fries;
    public Text price ;
    public void openPizza()
    {
        Pizza.gameObject.SetActive(true);
        Chicken.gameObject.SetActive(false);
        Coffee.gameObject.SetActive(false);
        Fries.gameObject.SetActive(false);
        price.text= "Price : 20 DT" ;
    }

    public void openChicken()
    {
        Chicken.gameObject.SetActive(true);
        Pizza.gameObject.SetActive(false);
        Coffee.gameObject.SetActive(false);
        Fries.gameObject.SetActive(false);
           price.text= "Price : 15 DT" ;
    }

    public void openCoffee()
    {
        Pizza.gameObject.SetActive(false);
        Chicken.gameObject.SetActive(false);
        Coffee.gameObject.SetActive(true);
        Fries.gameObject.SetActive(false);
        price.text= "Price : 6 DT" ;
    }

    public void openFries()
    {
        Pizza.gameObject.SetActive(false);
        Chicken.gameObject.SetActive(false);
        Coffee.gameObject.SetActive(false);
        Fries.gameObject.SetActive(true);
        price.text= "Price : 4 DT" ;
    }
}

