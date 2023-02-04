using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerStats : MonoBehaviour
{
    public int level = PlayerStats.level;
    public int experiencePoints = PlayerStats.experiencePoints;
    public int expToNextLevel = PlayerStats.expToNextLevel;

    public float health = PlayerStats.health;
    public float maxHealth = PlayerStats.maxHealth;
    public float healthMultiplier = PlayerStats.healthMultiplier;
    public float healthRegen = PlayerStats.healthRegen;

    public float damageMultiplier = PlayerStats.damageMultiplier;
    public float cooldownMultiplier = PlayerStats.cooldownMultiplier;
    public float movementSpeedMultiplier = PlayerStats.movementSpeedMultiplier;

    void FixedUpdate()
    {
         level = PlayerStats.level;
         experiencePoints = PlayerStats.experiencePoints;
         expToNextLevel = PlayerStats.expToNextLevel;

         health = PlayerStats.health;
         maxHealth = PlayerStats.maxHealth;
         healthMultiplier = PlayerStats.healthMultiplier;
         healthRegen = PlayerStats.healthRegen;

         damageMultiplier = PlayerStats.damageMultiplier;
         cooldownMultiplier = PlayerStats.cooldownMultiplier;
         movementSpeedMultiplier = PlayerStats.movementSpeedMultiplier;
    }

}
