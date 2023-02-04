using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    private Transform playerTransform;

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
            yield return new WaitForSeconds(WeaponStats.bubbleInterval * PlayerStats.cooldownMultiplier - (WeaponStats.bubbleProjectiles * 0.2f));
            
            for (int i = 1; i <= WeaponStats.bubbleProjectiles; i++)
            {
                Instantiate(bubblePrefab, playerTransform.position, Quaternion.identity);

                yield return new WaitForSeconds(0.2f);
            }          
        }       
    }

}
