using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRingScript : MonoBehaviour
{
    private List<string> damagedEnemies = new List<string>();
    [SerializeField] private float expansionRate;
    private float lifespan = 1.5f;

    void Awake()
    {
        transform.localScale *= WeaponStats.windRingSize;
        StartCoroutine(ProjectileLifespan());
    }

    void Update()
    {
        transform.localScale = transform.localScale * (1 + expansionRate * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!damagedEnemies.Contains(collision.gameObject.name))
        {
            DamageEnemy(collision.gameObject);
            damagedEnemies.Add(collision.gameObject.name);
        }
    }

    private void DamageEnemy(GameObject enemy)
    {
        IDamageable damageable = enemy.GetComponent<IDamageable>();

        damageable?.Damage(WeaponStats.windRingDamage * PlayerStats.damageMultiplier);
    }

    private IEnumerator ProjectileLifespan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
