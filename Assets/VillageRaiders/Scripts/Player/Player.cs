using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
    public class Player : Tower
{
    public Transform orb;
    public float lineDelay = .2f;
    ///public LineRenderer line;
    public static float curHealth = 100;
    public GameObject projectilePrefab;
    // My Health Slider
    public Slider healthBar;
    public Canvas myCanvas;
    public TowerType type;

    public enum TowerType
    {
        Base, Melee, Range
    }

    public void Start()

    {
        // Get Slider
        myCanvas = transform.Find("Canvas").GetComponent<Canvas>();
        healthBar = myCanvas.transform.Find("Slider").GetComponent<Slider>();
        curHealth = 100;
        healthBar.value = Mathf.Clamp01(curHealth / 100);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(orb.position, orb.position + orb.forward * 1000f);
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        healthBar.value = Mathf.Clamp01(curHealth / 100);

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Reset()
    {
        ///line = GetComponent<LineRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        // Follow Camera
        //myCanvas.transform.LookAt(Camera.main.transform);
        // If currentEnemy is null
        if (currentEnemy == null)
        {
            // Disable the line
            ///line.enabled = false;
        }
    }

    IEnumerator DisableLine()
    {
        yield return new WaitForSeconds(lineDelay);
        ///line.enabled = false;
    }

    public override void Aim(Enemy e)
    {
        // Get orb to look at enemy
        orb.LookAt(e.transform);
        // Create line from orb to enemy
        ///line.SetPosition(0, orb.position);
        ///line.SetPosition(1, e.transform.position);
    }

    public override void Attack(Enemy e)
    {
        // Enable the line
        ///line.enabled = true;
        // Deal damage to enemy
        e.TakeDamage(damage);
        // Run coroutine to disable the line on a delay
        StartCoroutine(DisableLine());
        // Spawn projectile at position and rotation of Player
        GameObject projectile = Instantiate(projectilePrefab);
        // Get Rigidbody2D from projectile
        Projectile bullet = projectile.GetComponent<Projectile>();
        bullet.Fire(orb.forward);

        projectile.transform.position = orb.position;
    }
}