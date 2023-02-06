using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePotionProjectileScript : MonoBehaviour
{
    [SerializeField] private GameObject unstablePotionPool;
    private Rigidbody2D rb;
    private Collider2D bottleCollider;
    private Collider2D targetingCollider;
    private Transform player;
    [SerializeField] private float throwForce;
    [SerializeField] private ContactFilter2D contactFilter;
    private Vector2 distanceToTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Wizard").GetComponent<Transform>();
        bottleCollider = GetComponent<Collider2D>();
        targetingCollider = GameObject.Find("AutoTargetingCollider").GetComponent<Collider2D>();
        TargetClosestEnemy();
        StartCoroutine(ProjectileLife());
    }

    void OnTriggerEnter2D()
    {
        bottleCollider.enabled = false;

        unstablePotionPool.SetActive(true);

        rb.velocity *= 0.0f;
    }

    //Autotargeting method
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

            rb.AddForce(distanceToTarget.normalized * throwForce, ForceMode2D.Impulse);
        }

        else
        {
            Vector2 randomVector = new Vector2(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f));

            rb.AddForce(randomVector.normalized * throwForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(1.0f);

        bottleCollider.enabled = false;

        unstablePotionPool.SetActive(true);

        rb.velocity *= 0.0f;
    }
}
