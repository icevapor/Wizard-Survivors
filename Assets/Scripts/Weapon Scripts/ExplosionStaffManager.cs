using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionStaffManager : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosionIndicator;
    [SerializeField] private float explosionOffset;
    private List<Collider2D> enemyList = new List<Collider2D>();

    void Awake()
    {
        StartCoroutine(ExplosionSpawn());
        player = GameObject.Find("Wizard").transform;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            explosionIndicator.transform.position = new Vector3(player.position.x, player.position.y - explosionOffset);
        }

        if (Input.GetKey(KeyCode.S))
        {
            explosionIndicator.transform.position = new Vector3(player.position.x, player.position.y + explosionOffset);
        }

        if (Input.GetKey(KeyCode.A))
        {
            explosionIndicator.transform.position = new Vector3(player.position.x + explosionOffset, player.position.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            explosionIndicator.transform.position = new Vector3(player.position.x - explosionOffset, player.position.y);
        }
    }

    private IEnumerator ExplosionSpawn()
    {
        while (GameManager.gameActive)
        {
            yield return new WaitForSeconds((WeaponStats.explosionInterval * PlayerStats.cooldownMultiplier) - 1.5f);

            explosionIndicator.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            Instantiate(explosionPrefab, explosionIndicator.transform.position, Quaternion.identity);

            explosionIndicator.SetActive(false);
        }        
    }

}
