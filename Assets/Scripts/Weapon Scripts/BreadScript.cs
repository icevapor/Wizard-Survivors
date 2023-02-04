using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadScript : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask layerMask;
    private GameObject currentEnemyTarget;
    private Vector2 distanceToTarget;
    [SerializeField] private float speed;
    [SerializeField] private float lungeForce;
    private bool hasTarget;
    private bool isAttacking;

    void Awake()
    {
        player = GameObject.Find("Wizard").GetComponent<Transform>();
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
            TargetNearestEnemy();
        }

        if (distanceToTarget.x > 0.0f)
        {
            spriteRenderer.flipX = true;
        }

        else
        {
            spriteRenderer.flipX = false;
        }

        if (!hasTarget && distanceToTarget.magnitude < 0.1f)
        {
            rb.velocity *= 0;
        }

        transform.localScale = new Vector3(WeaponStats.geeseSize, WeaponStats.geeseSize, WeaponStats.geeseSize);
    }

    //If no living target already exists, find one. If one has already been chosen, move towards it.
    private void TargetNearestEnemy()
    {
        if (currentEnemyTarget == null)
        {
            try
            {
                Vector2 circleCenter = new Vector2(player.position.x, player.position.y);

                float radius = 1.5f;

                //Line that may cause exception.
                Collider2D enemyCollider = Physics2D.OverlapCircle(circleCenter, radius, layerMask);

                distanceToTarget = new Vector2(enemyCollider.transform.position.x - transform.position.x, enemyCollider.transform.position.y - transform.position.y);

                currentEnemyTarget = enemyCollider.gameObject;

                hasTarget = true;
            }

            catch (NullReferenceException e)
            {
                hasTarget = false;

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
