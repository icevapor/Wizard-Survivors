using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRingManager : MonoBehaviour
{
    [SerializeField] private GameObject windProjectilePrefab;

    void Awake()
    {
        StartCoroutine(WindProjectileSpawn());
    }

    private IEnumerator WindProjectileSpawn()
    {
        while (GameManager.gameActive)
        {
            yield return new WaitForSeconds(WeaponStats.windRingInterval * PlayerStats.cooldownMultiplier);

            Instantiate(windProjectilePrefab, transform);          
        }
    }
}
