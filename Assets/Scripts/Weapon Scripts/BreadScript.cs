using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadScript : MonoBehaviour
{
    private Transform player;
    private Collider2D targetingCollider;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private ContactFilter2D contactFilter;
    private GameObject currentEnemyTarget;
    private Vector2 distanceToTarget;
    [SerializeField] private float speed;
    [SerializeField] private float lungeForce;
    private bool isAttacking;

    void Awake()
    {
        player = GameObject.Find("Wizard").GetComponent<Transform>();
        targetingCollider = GameObject.Find("AutoTargetingCollider").GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed *= UnityEngine.Random.Range(0.9f, 1.1f);
    }

    void Update()
    {
        if (distanceToTarget.magnitude < 0.5f && !isAttacking && currentEnemyTarget != null)
        {
            distanceToTarget = new Vector2(currentEnemyTarget.transform.position.x - transform.position.x, currentEnemyTarget.transform.position.y - transform.position.y);

            StartCoroutine(AttackEnemy(currentEnemyTarget));
        }

        else if (!isAttacking)
        {
            TargetClosestEnemy();
        }

        if (distanceToTarget.x > 0.0f)
        {
            spriteRenderer.flipX = true;
        }

        else
        {
            spriteRenderer.flipX = false;
        }

        if (currentEnemyTarget == null && distanceToTarget.magnitude < 0.1f)
        {
            rb.velocity *= 0;
        }

        transform.localScale = new Vector3(WeaponStats.duckSize, WeaponStats.duckSize, WeaponStats.duckSize);
    }

    private void TargetClosestEnemy()
    {
        if (currentEnemyTarget == null)
        {
            Collider2D[] results = new Collider2D[32];
            int nearbyEnemies = Physics2D.OverlapCollider(targetingCollider, contactFilter, results);

            if (nearbyEnemies != 0)
            {
                int colliderIndex = 0;
                float closestDistance = 100.0f;

                for (int i = 0; i < nearbyEnemies; i++)
                {
                    float distance = (results[i].transform.position - transform.position).magnitude;

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        colliderIndex = i;
                    }
                }

                currentEnemyTarget = results[colliderIndex].gameObject;
            }

            else
            {
                distanceToTarget = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

                rb.velocity = (distanceToTarget.normalized / 1.5f) + (distanceToTarget * speed * (1 / WeaponStats.breadSpeedMultiplier));
            }
        }

        else
        {
            distanceToTarget = new Vector2(currentEnemyTarget.transform.position.x - transform.position.x, currentEnemyTarget.transform.position.y - transform.position.y);

            rb.velocity = (distanceToTarget.normalized / 1.5f) + (distanceToTarget * speed * (1 / WeaponStats.breadSpeedMultiplier));
        }     
    }

    private IEnumerator AttackEnemy(GameObject enemy)
    {
        isAttacking = true;

        //Backs up for a moment
        rb.AddForce(distanceToTarget.normalized * lungeForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f * PlayerStats.cooldownMultiplier * WeaponStats.breadSpeedMultiplier);

        rb.velocity *= 0;

        yield return new WaitForSeconds(0.5f * PlayerStats.cooldownMultiplier * WeaponStats.breadSpeedMultiplier);

        isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (isAttacking)
        {
            IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();

            damageable?.Damage(WeaponStats.breadDamage * PlayerStats.damageMultiplier);
        }
    }

}
