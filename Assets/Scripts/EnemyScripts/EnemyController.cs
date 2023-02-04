using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject expPickupPrefab;
    [SerializeField] private Sprite damagedSprite;
    private Sprite normalSprite;
    private SpriteRenderer spriteRenderer;
    private SpawnManager spawnManager;
    private Rigidbody2D rb;
    private GameObject player;

    public float health;
    public float speed;
    [SerializeField] private float attackDamage;
    private bool attackCooldown;

    Vector2 distanceToTarget;

    private GameObject currentEnemyTarget;
    private EnemyController currentEnemyController;
    [SerializeField] private LayerMask layerMask;
    public bool isPoisoned;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = spriteRenderer.sprite;
        player = GameObject.Find("Wizard");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        //Randomize stats
        speed *= UnityEngine.Random.Range(0.8f, 1.2f);
        health *= UnityEngine.Random.Range(0.8f, 1.2f);
        transform.localScale *= UnityEngine.Random.Range(0.8f, 1.2f);
    }

    void Update()
    {
        //If poisoned, the target is the nearest enemy.
        if (isPoisoned && !attackCooldown)
        {
            TargetNearestEnemy();
        }

        //If not poisoned, the target is the player.
        else if (!isPoisoned)
        {
            TargetPlayer();
        }

        if (distanceToTarget.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attackCooldown && isPoisoned && collision.gameObject.layer == 6)
        {
            StartCoroutine(DealDamageToEnemy(collision.gameObject));
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!attackCooldown && isPoisoned && collision.gameObject.layer == 6)
        {
            StartCoroutine(DealDamageToEnemy(collision.gameObject));
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!attackCooldown && !isPoisoned)
        {
            StartCoroutine(DealDamageToPlayer(collider.gameObject));
        }
    }

    //Quite simply gets the vector pointing towards the player and sets the velocity to move in that direction.
    private void TargetPlayer()
    {
        distanceToTarget = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        rb.velocity = distanceToTarget.normalized * speed;
    }

    private void TargetNearestEnemy()
    {
        if (currentEnemyTarget == null)
        {
            //Scans a circular area around the player and targets the first collider found. If no collider is found, a random vector is generated as a target direction.
            try
            {
                Vector2 circleCenter = new Vector2(transform.position.x, transform.position.y);

                float radius = 4.0f;

                //Line that may cause exception.
                Collider2D enemyCollider = Physics2D.OverlapCircle(circleCenter, radius, layerMask);

                distanceToTarget = new Vector2(enemyCollider.transform.position.x - transform.position.x, enemyCollider.transform.position.y - transform.position.y);

                currentEnemyTarget = enemyCollider.gameObject;

                currentEnemyController = currentEnemyTarget.GetComponent<EnemyController>();
            }

            catch (NullReferenceException e)
            {
                distanceToTarget = new Vector2(transform.position.x + UnityEngine.Random.Range(-4.0f, 4.0f), transform.position.y + UnityEngine.Random.Range(-4.0f, 4.0f));
            }

            rb.velocity = distanceToTarget.normalized * speed;
        }

        else if (currentEnemyController.isPoisoned)
        {
            currentEnemyTarget = null;
        }

        else
        {
            distanceToTarget = new Vector2(currentEnemyTarget.transform.position.x - transform.position.x, currentEnemyTarget.transform.position.y - transform.position.y);
            rb.velocity = distanceToTarget.normalized * speed;
        }
    }

    private IEnumerator DealDamageToPlayer(GameObject enemy)
    {
        attackCooldown = true;

        IDamageable damageable = enemy.GetComponent<IDamageable>();

        damageable?.Damage(attackDamage);

        yield return new WaitForSeconds(1.0f);

        attackCooldown = false;
    }

    private IEnumerator DealDamageToEnemy(GameObject enemy)
    {
        attackCooldown = true;

        IDamageable damageable = enemy.GetComponent<IDamageable>();

        damageable?.Damage((attackDamage + WeaponStats.unstablePotionBonusDamage) * PlayerStats.damageMultiplier);

        yield return new WaitForSeconds(0.5f * PlayerStats.cooldownMultiplier);

        attackCooldown = false;
    }

    public void Damage(float damage)
    {
        if (!isPoisoned)
        {
            health -= damage;

            if (health <= 0)
            {
                Instantiate(expPickupPrefab, transform.position, Quaternion.identity);
                spawnManager.currentEnemies -= 1;
                Destroy(gameObject);
            }

            StartCoroutine(DamagedBlink());
        }      
    }

    private IEnumerator DamagedBlink()
    {
        spriteRenderer.sprite = damagedSprite;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = normalSprite;
    }

}
