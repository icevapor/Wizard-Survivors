using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, IDamageable
{
    private GameObject player;
    private Rigidbody2D rb;

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float contactDamage;
    private bool hasAttacked;

    void Start()
    {
        player = GameObject.Find("Wizard");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
    }

    private IEnumerator AttackCooldown()
    {
        hasAttacked = true;

        yield return new WaitForSeconds(2.0f);

        hasAttacked = false;
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 && !hasAttacked)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            damageable?.Damage(contactDamage);

            StartCoroutine(AttackCooldown());
        }
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
