using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePotionManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    
    void Awake()
    {
        StartCoroutine(ProjectileSpawn());
    }

    private IEnumerator ProjectileSpawn()
    {
        while (GameManager.gameActive)
        {
            Vector2 spawnPos = new Vector2 (transform.position.x, transform.position.y);
            Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(WeaponStats.unstablePotionInterval * PlayerStats.cooldownMultiplier);
        }    
    }

}
