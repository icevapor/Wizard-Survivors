using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadManager : MonoBehaviour
{
    [SerializeField] private GameObject duckPrefab;
    private int currentDucks;

    void Awake()
    {
        StartCoroutine(SpawnDucks());
    }

    private IEnumerator SpawnDucks()
    {
        while (GameManager.gameActive)
        {
            if (currentDucks < WeaponStats.maxDucks)
            {
                Instantiate(duckPrefab, transform.position, Quaternion.identity);
                currentDucks++;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
