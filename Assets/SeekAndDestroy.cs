using System.Collections.Generic;
using UnityEngine;

public class SeekAndDestroy : MonoBehaviour
{
    public Transform enemySpawner;
    private List<GameObject> enemyNPCs = new List<GameObject>();

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
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
}
