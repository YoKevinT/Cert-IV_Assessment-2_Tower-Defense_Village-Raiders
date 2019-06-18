using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillAll : MonoBehaviour
{
    public void DestroyObjects(string tag)
    {
            Money.price = 100;
        if (Money.price <= Money.money && Time.timeScale == 1f)
        {
            Money.money -= Money.price;
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject target in gameObjects)
            {
                GameObject.Destroy(target);
            }
        }
    }
}