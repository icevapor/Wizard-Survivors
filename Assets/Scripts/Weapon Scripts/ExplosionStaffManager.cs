using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionStaffManager : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D playerRb;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosionIndicator;
    [SerializeField] private float explosionOffset;
    private Vector3 lastIndicatorPosition;
    private List<Collider2D> enemyList = new List<Collider2D>();

    void Awake()
    {
        StartCoroutine(ExplosionSpawn());
        player = GameObject.Find("Wizard").transform;
        playerRb = GameObject.Find("Wizard").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerRb.velocity.magnitude > 0.0f)
        {
            lastIndicatorPosition = -playerRb.velocity.normalized * explosionOffset;
            explosionIndicator.transform.localPosition = lastIndicatorPosition;
        }
        
        else if (lastIndicatorPosition.magnitude != 0f)
        {
            explosionIndicator.transform.localPosition = lastIndicatorPosition;
        }

        else
        {
            explosionIndicator.transform.localPosition = new Vector3(-0.75f, 0.0f);
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
