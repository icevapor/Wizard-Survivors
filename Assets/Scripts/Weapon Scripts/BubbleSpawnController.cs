using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    private Transform playerTransform;
    [SerializeField] private float weaponCooldown;

    void Awake()
    {
        StartCoroutine("BubbleSpawn");
        playerTransform = GameObject.Find("Wizard").GetComponent<Transform>(); 
    }

    //Continuously spawns bubble projectiles at an interval of weaponCooldown.
    private IEnumerator BubbleSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(weaponCooldown);
            Instantiate(bubblePrefab, playerTransform.position, Quaternion.identity);
        }       
    }

}
