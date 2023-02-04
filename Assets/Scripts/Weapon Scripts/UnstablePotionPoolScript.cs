using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePotionPoolScript : MonoBehaviour
{
    [SerializeField] private GameObject unstablePotionParent;
    [SerializeField] private GameObject unstablePotionProjectile;
    private SpawnManager spawnManager;
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] poisonedSprites;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        unstablePotionProjectile.SetActive(false);
        StartCoroutine("ProjectileLife");
    }

    //When an enemy collides with the pool (because of the way the collision matrix is set up only enemies will collide with it) the pool's collider and sprite renderer turn off,
    //but the script continues to function until the enemy dies of the poison.
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 6)
        {
            if (collider.gameObject.CompareTag("SpecialEnemy"))
            {
                return;
            }

            EnemyController enemyCon = collider.gameObject.GetComponent<EnemyController>();

            if (!enemyCon.isPoisoned)
            {
                StartCoroutine(DamageEnemy(collider.gameObject));
                enemyCon.isPoisoned = true;
                enemyCon.speed *= 2f;

                //Collider and renderer turn off, but gameobject remains active until enemy dies.
                circleCollider.enabled = false;
                spriteRenderer.enabled = false;
                StopCoroutine("ProjectileLife");
            }
        }
        
        else if (collider.gameObject.layer == 11)
        {
            PuppetController puppetCon = collider.gameObject.GetComponent<PuppetController>();

            circleCollider.enabled = false;

            spriteRenderer.enabled = false;

            StopCoroutine("ProjectileLife");

            StartCoroutine(BossPoisoned(puppetCon));

            StartCoroutine(DamageBoss(puppetCon));
        }
    }

    //This coroutine continues to damage the enemy periodically until they die.
    private IEnumerator DamageEnemy(GameObject enemy)
    {
        SpriteRenderer enemySpriteRenderer = enemy?.GetComponent<SpriteRenderer>();

        //Makes the enemy green and large.
        foreach (Sprite sprite in poisonedSprites)
        {
            if (sprite.name == "Poisoned" + enemySpriteRenderer.sprite.name)
            {
                enemySpriteRenderer.sprite = sprite;
            }
        }

        enemy.gameObject.transform.localScale *= 1.5f;

        enemy.gameObject.layer = 10;

        yield return new WaitForSeconds(WeaponStats.unstablePotionEffectDuration);

        spawnManager.currentEnemies -= 1;

        EnemyController enemyCon = enemy?.GetComponent<EnemyController>();

        enemyCon.isPoisoned = false;

        IDamageable damageable = enemy?.GetComponent<IDamageable>();

        damageable?.Damage(9999f);

        Destroy(unstablePotionParent);
    }

    private IEnumerator BossPoisoned(PuppetController puppetCon)
    {
        puppetCon.isPoisoned = true;

        yield return new WaitForSeconds(WeaponStats.unstablePotionEffectDuration);

        puppetCon.isPoisoned = false;
    }

    private IEnumerator DamageBoss(PuppetController puppetCon)
    {
        while (puppetCon.isPoisoned)
        {
            puppetCon.Damage((WeaponStats.unstablePotionBossDamage) * PlayerStats.damageMultiplier);

            yield return new WaitForSeconds(1.0f * PlayerStats.cooldownMultiplier);
        }
    }

    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(unstablePotionParent);
    }
}
