using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetController : MonoBehaviour, IDamageable
{
    private GameObject player;
    private BossHealthBar bossHealthBar;
    [SerializeField] private GameObject spiderPrefab;
    [SerializeField] private GameObject projectilePrefab;
    private Transform projectileOrigin;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] puppetSprites = new Sprite[4]; //0 is default, 1 is mouth open, 2 is default damaged, 3 is mouth open damaged.
    private Rigidbody2D rb;

    public float health;
    public float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float contactDamage;
    [SerializeField] private float chargeForce;

    public bool isPoisoned;

    private bool hasAttacked;
    private bool isAttacking;
    private bool isCharging;
    private bool mouthOpen;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 mouthOffset;
    private Vector3 distanceToPlayer;
    
    void Start()
    {
        player = GameObject.Find("Wizard");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        projectileOrigin = GameObject.Find("ProjectileOrigin").transform;
        bossHealthBar = GameObject.Find("BossHealthBar").GetComponent<BossHealthBar>();
        bossHealthBar.HealthBarOn();
    }

    void Update()
    {
        //If not charging, move toward player at constant speed.
        if (!isCharging)
        {
            TargetPlayer();
        }      

        //If not currently attacking and not on attack cooldown, use a random attack.
        if (!hasAttacked && !isAttacking)
        {
            PickRandomAttack();
        }
    }

    //Gets distance to player and sets velocity accordingly.
    private void TargetPlayer()
    {
        distanceToPlayer = player.transform.position - (transform.position + offset);

        if (isAttacking)
        {
            rb.velocity = distanceToPlayer.normalized * speed / 2.0f;
            return;
        }

        rb.velocity = distanceToPlayer.normalized * speed;
    }

    //IDamageable implemented damage method.
    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            bossHealthBar.HealthBarOff();

            Destroy(gameObject);
        }

        StartCoroutine(DamagedBlink());
    }

    //Blinks when taking damage.
    private IEnumerator DamagedBlink()
    {
        if (mouthOpen)
        {
            spriteRenderer.sprite = puppetSprites[3];

            yield return new WaitForSeconds(0.2f);

            spriteRenderer.sprite = puppetSprites[1];
        }

        else
        {
            spriteRenderer.sprite = puppetSprites[2];

            yield return new WaitForSeconds(0.2f);

            spriteRenderer.sprite = puppetSprites[0];
        }
    }

    //Deals damage to the player upon collision.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            damageable?.Damage(contactDamage);
        }
    }

    //Telegraphs the charge by jiggling around erratically, then stops and charges at the player. There is a window of time where the player can sidestep the charge, although it may be hard if
    //the boss is too close or if the player is too slow.
    private IEnumerator Charge()
    {
        isAttacking = true;

        hasAttacked = true;

        for (int i = 0; i < 10; i++)
        {
            transform.position += new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));

            yield return new WaitForSeconds(0.08f);
        }

        isCharging = true;

        yield return new WaitForSeconds(0.15f);

        Vector3 chargeTarget = player.transform.position - (transform.position + offset);

        yield return new WaitForSeconds(0.15f);

        rb.AddForce(chargeTarget.normalized * chargeForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1.0f);

        rb.velocity *= 0f;

        isCharging = false;

        isAttacking = false;

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator SummonSpiders()
    {
        isAttacking = true;

        hasAttacked = true;

        mouthOpen = true;

        spriteRenderer.sprite = puppetSprites[1];

        for (int i = 0; i < 14; i++)
        {
            GameObject newSpider = Instantiate(spiderPrefab, transform.position + mouthOffset, Quaternion.identity);

            newSpider.name = newSpider.name + Random.Range(0, 9) + Random.Range(0, 9) + Random.Range(0, 9) + Random.Range(0, 9);

            yield return new WaitForSeconds(0.5f);
        }

        spriteRenderer.sprite = puppetSprites[0];

        mouthOpen = false;

        isAttacking = false;

        StartCoroutine(AttackCooldown());
    }

    //Puppet opens mouth and fires 5 projectiles after waiting 1 second.
    private IEnumerator FireProjectiles()
    {
        isAttacking = true;

        hasAttacked = true;

        mouthOpen = true;

        spriteRenderer.sprite = puppetSprites[1];

        yield return new WaitForSeconds(1f);

        projectileOrigin.right = player.transform.position - projectileOrigin.position;

        //Instantiate 5 projectiles and label each one so that they know which pointer they coorespond to.
        for (int i = 0; i < 5; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position + mouthOffset, Quaternion.identity);

            PuppetProjectile puppetProjectile = newProjectile.GetComponent<PuppetProjectile>();

            puppetProjectile.identity = i;
        }

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.sprite = puppetSprites[0];

        mouthOpen = false;

        isAttacking = false;

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(5.0f);

        hasAttacked = false;
    }

    private void PickRandomAttack()
    {
        int attackChoice = Random.Range(0, 3);
        switch (attackChoice)
        {
            case 0:
                StartCoroutine(Charge());

                break;

            case 1:
                StartCoroutine(SummonSpiders());

                break;
            
            case 2:
                StartCoroutine(FireProjectiles());

                break;
        }
    }
}
