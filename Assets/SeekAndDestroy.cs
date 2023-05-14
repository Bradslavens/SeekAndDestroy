using System.Collections.Generic;
using UnityEngine;

public class SeekAndDestroy : MonoBehaviour
{
    public Transform enemySpawner;
    private List<GameObject> enemyNPCs = new List<GameObject>();

    public GameObject projectilePrefab;
    public Transform muzzle; // Reference to the child object named "Muzzle"
    public float fireInterval = 1f;
    private float fireTimer;

    private void Start()
    {
        // Populate the enemyNPCs list when the script starts
        PopulateEnemyNPCs();
    }

    private void PopulateEnemyNPCs()
    {
        foreach (Transform child in enemySpawner)
        {
            if (child.CompareTag("Enemy"))
            {
                enemyNPCs.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        // Example usage: Find closest enemy and rotate towards it
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            RotateTowardsEnemy(closestEnemy);
            FireAtEnemy();
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemyNPC in enemyNPCs)
        {
            float distance = Vector3.Distance(transform.position, enemyNPC.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemyNPC;
            }
        }
        return closestEnemy;
    }

    private void RotateTowardsEnemy(GameObject enemy)
    {
       Vector3 direction = enemy.transform.position - transform.position;
        direction.y = 0f; // Set the y-component to zero to restrict rotation to the y-axis only
       Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void FireAtEnemy()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            // Instantiate a projectile and set its position and rotation
            GameObject projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation); // Use the muzzle's position and rotation

            // Get the projectile's Rigidbody component
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

            // Set the speed of the projectile
            float projectileSpeed = 10f; // Adjust the speed as desired

            // Calculate the movement vector based on the projectile's local forward direction
            Vector3 velocity = projectile.transform.forward * projectileSpeed;

            // Set the velocity of the projectile's Rigidbody in local space
            projectileRigidbody.velocity = velocity;

            // Reset the timer
            fireTimer = 0f;
        }
    }
}
