using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private SpawnManager spawnManager;
    private Rigidbody2D rb;
    private GameObject player;
    public float health;
    [SerializeField] private float speed;
    Vector2 distanceToPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Wizard");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        distanceToPlayer = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        rb.velocity = distanceToPlayer.normalized * speed;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            spawnManager.currentEnemies -= 1;
            Destroy(gameObject);
        }
    }

}
