using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {

    public static int money = 0;
    public Text MoneyText;
    public static int price;

    void Start()
    {
        money = 100;
        price = 10;
        MoneyText = GetComponent<Text>();
    }

    void Update()
    {
        MoneyText.text = "      " + money;
    }
}
