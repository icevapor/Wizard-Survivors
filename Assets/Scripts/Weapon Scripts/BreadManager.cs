using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadManager : MonoBehaviour
{
    [SerializeField] private GameObject goosePrefab;
    private int currentGeese;

    void Awake()
    {
        StartCoroutine(SpawnGeese());
    }

    private IEnumerator SpawnGeese()
    {
        while (GameManager.gameActive)
        {
            if (currentGeese < WeaponStats.maxGeese)
            {
                Instantiate(goosePrefab, transform.position, Quaternion.identity);
                currentGeese++;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
