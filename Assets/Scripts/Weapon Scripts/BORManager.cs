using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BORManager : MonoBehaviour
{
    [SerializeField] private GameObject[] orbitals = new GameObject[4];
    private int activeOrbitals;

    void Update()
    {
        if (activeOrbitals < WeaponStats.maxOrbitals)
        {
            orbitals[activeOrbitals].SetActive(true);

            activeOrbitals += 1;
        }

        transform.Rotate(new Vector3(0f, 0f, WeaponStats.orbitalRevolutionSpeed) * Time.deltaTime * PlayerStats.cooldownMultiplier);
    }
}
