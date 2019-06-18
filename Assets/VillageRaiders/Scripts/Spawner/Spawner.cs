using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int amountOfEnemys;
    public int wave;
    public bool canSpawn;
    //the enemy we want to spawn in as a list
    public GameObject[] enemyToSpawn;
    //the selected enemy we want to spawn
    public int enemyIndex;
    //time that we want to spawn the enemy
    public float spawnTimer;
    //the count up timer
    public float timer;
    void Start()
    {
        //Ranger with integer is not exclusive with the Max.(max not included)
        //Only Range with float is inclusive with the Max.(max is included)
        spawnTimer = 1;
        amountOfEnemys = 3;
        canSpawn = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            NextWave();
        }
        if(amountOfEnemys <= 0)
        {
            canSpawn = false;
        }
        if(canSpawn)
        {
            timer += Time.deltaTime;

            if (timer >= spawnTimer)
            {
                enemyIndex = Random.Range(0, enemyToSpawn.Length);
                Instantiate(enemyToSpawn[enemyIndex], this.transform.position, Quaternion.identity);
                timer = 0;
                //Ranger with integer is not exclusive with the Max.(max not included)
                //Only Range with float is inclusive with the Max.(max is included)
                amountOfEnemys--;
            }
        }
       
    }
    public void NextWave()
    {
        wave++;
        amountOfEnemys += (5  + wave*4);
        canSpawn = true;
    }
}