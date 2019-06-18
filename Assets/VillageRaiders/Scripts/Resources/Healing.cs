using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Healing : MonoBehaviour
{
    public float healing;
    public void HealObjects(string tag)
    {
        Money.price = 100;
        if (Money.price <= Money.money && Time.timeScale == 1f)
        {
            Money.money -= Money.price;
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject target in gameObjects)
            {
                target.GetComponent<Player>().Start();               
            }
        }
    }
}