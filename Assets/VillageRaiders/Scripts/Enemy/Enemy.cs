using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    //public Transform target;
    public Transform target;
    public Transform targetedTarget;
    private NavMeshAgent agent; // Reference to NavMeshAgent component
    private int health = 100;
    public int damage = 30;
    private Player player;
    private float attackTimer;
    public int price;

    void Start()
    {
        // Set health to whatever maxHealth is at start
        health = maxHealth;
        // Getting NavMeshAgent from list of components
        agent = GetComponent<NavMeshAgent>();
        targetedTarget = target;
    }

    // Call this function to make Enemy take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ScoreScript.scoreValue += 1000;
            Money.money += price;
            //Debug.Log(Money.money);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Tell NavMeshAgent to follow target position
        if (targetedTarget == null)
        {
            targetedTarget = target;
        }
        agent.SetDestination(targetedTarget.position);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            targetedTarget = collision.transform;
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 0.5f)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                attackTimer = 0;
            }
        }
        if (collision.gameObject.CompareTag("EndZone"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
  
}