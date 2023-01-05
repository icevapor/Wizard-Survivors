using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite poppedSprite;

    [SerializeField] private float projectileForce;
    [SerializeField] private float projectileLifespan;
    [SerializeField] private int projectileDamage;

    private Vector2 distanceToTarget;
    private Transform player;

    [SerializeField] private LayerMask layerMask;

    void Awake()
    {
        StartCoroutine("ProjectileLife");

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Wizard").GetComponent<Transform>();

        //Scans a square area around the player and targets the first collider found. If no collider is found, a random vector is generated as a target direction.
        try
        {
            Vector2 circleCenter = new Vector2(player.position.x, player.position.y);

            float radius = 15.0f;

            //Line that may cause exception.
            Collider2D enemyCollider = Physics2D.OverlapCircle(circleCenter, radius, layerMask);

            Debug.Log(enemyCollider);

            distanceToTarget = new Vector2(enemyCollider.transform.position.x - transform.position.x, enemyCollider.transform.position.y - transform.position.y);

            rb.AddForce(distanceToTarget.normalized * projectileForce, ForceMode2D.Impulse);
        }

        catch(NullReferenceException e) 
        {
            Debug.Log("Detect Fail");
            Vector2 randomVector = new Vector2(player.position.x + UnityEngine.Random.Range(-15.0f, 15.0f), player.position.y + UnityEngine.Random.Range(-15.0f, 15.0f));

            rb.AddForce(randomVector * projectileForce, ForceMode2D.Impulse);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 6)
        {
            StartCoroutine(EnemyHit(collider.gameObject));
        }
    }

    //On hitting an enemy change sprite to poppedSprite, stop momentum, damage enemy, and then terminate after half a second.
    private IEnumerator EnemyHit(GameObject enemy)
    {
        IDamageable damageable = enemy.GetComponent<IDamageable>();

        damageable?.Damage(projectileDamage);

        spriteRenderer.sprite = poppedSprite;

        rb.velocity = new Vector2(0.0f, 0.0f);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    //If projectile doesn't collide with an enemy and terminate that way, it will automatically terminate after projectileLifespan seconds.
    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(projectileLifespan);
        Destroy(gameObject);
    }
}
