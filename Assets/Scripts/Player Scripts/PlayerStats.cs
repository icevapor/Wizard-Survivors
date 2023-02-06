using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int level = 0;
    public static int maxLevel = 30;
    public static int experiencePoints = 0;
    public static int expToNextLevel = 10;

    public static float health = 30;
    public static float maxHealth = 30;
    public static float defaultHealth = 30;
    public static float healthMultiplier = 1.0f;
    public static float healthRegen = 0.0025f;

    public static float damageMultiplier = 1.0f;
    public static float cooldownMultiplier = 1.0f;
    public static float movementSpeedMultiplier = 1.0f;

    public static int currentWeapons = 0;
    public static int maxWeapons = 3;
    public static int[] ownedWeapons = new int[] {0, 0, 0};
    public static int currentPassives = 0;
    public static int maxPassives = 3;
    public static int[] ownedPassives = new int[3];
}
