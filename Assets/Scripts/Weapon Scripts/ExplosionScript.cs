using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private List<string> damagedEnemies = new List<string>();

     void Awake()
    {
        transform.localScale *= WeaponStats.explosionSize;
        StartCoroutine(ExplosionLifespan());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!damagedEnemies.Contains(collider.name))
        {
            DamageEnemy(collider.gameObject);
            damagedEnemies.Add(collider.name);
        }       
    }

    private void DamageEnemy(GameObject enemy)
    {
        IDamageable damageable = enemy.GetComponent<IDamageable>();

        damageable?.Damage(WeaponStats.explosionDamage * PlayerStats.damageMultiplier);
    }

    private IEnumerator ExplosionLifespan()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
