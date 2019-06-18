using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public GameObject[] towers;
    public GameObject[] holograms;
    [Header("Raycasts")]
    public float rayDistance = 1000f;
    public LayerMask hitLayers;
    public QueryTriggerInteraction triggerInteraction;

    public static int currentIndex; // Current prefab selected

    void DrawRay(Ray ray)
    {
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f);
    }

    // Use this for initialization
    void OnDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Ray playerRay = new Ray(transform.position, transform.forward);
        //float angle = Vector3.Angle(mouseRay.direction, playerRay.direction);
        //print(angle);
        Gizmos.color = Color.white;
        DrawRay(mouseRay);
        Gizmos.color = Color.red;
        DrawRay(playerRay);
    }

    void Update()
    {

        // Disable all Holograms at the start of the frame
        DisableAllHolograms();

        // Create ray from mouse position on Camera
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Perform Raycast
        if (Physics.Raycast(mouseRay, out hit, rayDistance, hitLayers, triggerInteraction))
        {
            // Try getting Placeable script
            Placeable p = hit.transform.GetComponent<Placeable>();
            // If it is a placeable AND it's available (no tower spawned)

            if (p && p.isAvailable && Time.timeScale == 1f)
            {
                // Get hologram of current tower
                GameObject hologram = holograms[currentIndex];
                hologram.SetActive(true);
                // Set position of hologram to pivot point (if any)
                hologram.transform.position = p.GetPivotPoint();
                // Get the prefab
                GameObject towerPrefab = towers[currentIndex];
                Player.TowerType type = towerPrefab.GetComponent<Player>().type;
                //place range
                if (type == Player.TowerType.Range)
                {
                    // If Left mouse is down 
                    if (p.GetComponent<Player>() != null)
                    {
                        if (Input.GetMouseButtonDown(0) && p.GetComponent<Player>().type == Player.TowerType.Base)
                        {
                            if (Money.price <= Money.money)
                            {
                                Money.money -= Money.price;
                                // Spawn the tower
                                GameObject tower = Instantiate(towerPrefab);
                                // Position to placeable
                                tower.transform.position = p.GetPivotPoint();
                                // The Tile is no longer available
                                p.isAvailable = false;
                            }
                        }
                    }

                } //if base or melee place but not on another base
                else if (type != Player.TowerType.Range)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (p.GetComponent<Player>() == null)
                        {
                            if (Money.price <= Money.money)
                            {
                                Money.money -= Money.price;
                                // Spawn the tower
                                GameObject tower = Instantiate(towerPrefab);
                                // Position to placeable
                                tower.transform.position = p.GetPivotPoint();
                                // The Tile is no longer available
                                p.isAvailable = false;
                            }
                        }

                    }
                }
            }
        }
    }

    /// <summary>
    /// Disables the GameObjects of all referenced Holograms
    /// </summary>
    void DisableAllHolograms()
    {
        foreach (var holo in holograms)
        {
            holo.SetActive(false);
        }
    }

    /// <summary>
    /// Changes currentIndex to selected index 
    /// with filters
    /// </summary>
    /// <param name="index">The index we want to change to</param>
    public void SelectTower(int index)
    {
        // Is index in range of prefabs
        if (index >= 0 && index < towers.Length && Time.timeScale == 1f)
        {
            // Set current index
            currentIndex = index;
        }
        switch(currentIndex)
        {
            case 0:
                Money.price = 10;
                break;
            case 1:
                Money.price = 25;
                break;
            case 2:
                Money.price = 50;
                break;
            default:
                Money.price = 10;
                break;
        }  
    }
}