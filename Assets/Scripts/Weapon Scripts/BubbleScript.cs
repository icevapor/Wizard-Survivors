using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D targetingCollider;
    [SerializeField] private Sprite poppedSprite;

    [SerializeField] private float projectileForce;
    [SerializeField] private float projectileLifespan;

    private Vector2 distanceToTarget;

    [SerializeField] private ContactFilter2D contactFilter;

    void Start()
    {
        StartCoroutine("ProjectileLife");

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        targetingCollider = GameObject.Find("AutoTargetingCollider").GetComponent<Collider2D>();

        TargetClosestEnemy();      
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(EnemyHit(collider.gameObject));
    }

    //On hitting an enemy change sprite to poppedSprite, stop momentum, damage enemy, and then terminate after half a second.
    private IEnumerator EnemyHit(GameObject enemy)
    {
        IDamageable damageable = enemy.GetComponent<IDamageable>();

        damageable?.Damage(WeaponStats.bubbleDamage * PlayerStats.damageMultiplier);

        spriteRenderer.sprite = poppedSprite;

        rb.velocity = new Vector2(0.0f, 0.0f);

        yield return new WaitForSeconds(0.25f);

        Destroy(gameObject);
    }

    //If projectile doesn't collide with an enemy and terminate that way, it will automatically terminate after projectileLifespan seconds.
    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(projectileLifespan);
        Destroy(gameObject);
    }

    private void TargetClosestEnemy()
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

            distanceToTarget = new Vector2(results[colliderIndex].transform.position.x - transform.position.x, results[colliderIndex].transform.position.y - transform.position.y);

            rb.AddForce(distanceToTarget.normalized * projectileForce, ForceMode2D.Impulse);
        }

        else
        {
            Vector2 randomVector = new Vector2(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f));

            rb.AddForce(randomVector.normalized * projectileForce, ForceMode2D.Impulse);
        }
    }
}
