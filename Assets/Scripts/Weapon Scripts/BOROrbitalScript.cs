using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOROrbitalScript : MonoBehaviour
{
    private List<string> damagedEnemies = new List<string>();
    private float amountRotated;

    void Update()
    {
        amountRotated += WeaponStats.orbitalRevolutionSpeed * Time.deltaTime * PlayerStats.cooldownMultiplier;

        if (amountRotated >= 360.0f)
        {
            damagedEnemies.Clear();
        }
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

        damageable?.Damage(WeaponStats.orbitalDamage * PlayerStats.damageMultiplier);
    }
}
