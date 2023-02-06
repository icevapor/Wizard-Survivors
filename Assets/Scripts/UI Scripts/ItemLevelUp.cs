using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevelUp : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons = new GameObject[5];
    private PlayerInventory pInventory;
    private LevelUpUIManager levelUpManager;

    void Start()
    {
        pInventory = GameObject.Find("Wizard").GetComponent<PlayerInventory>();
        levelUpManager = GetComponent<LevelUpUIManager>();
    }

    public void LevelUpPassiveItem(int itemIndex)
    {
        switch (itemIndex)
        {
            case 6:
                pInventory.itemLevels[6] += 1;

                PlayerStats.healthRegen += 0.0025f;

                break;

            case 7:
                pInventory.itemLevels[7] += 1;

                PlayerStats.movementSpeedMultiplier += 0.07f;

                break;

            case 8:
                pInventory.itemLevels[8] += 1;

                PlayerStats.damageMultiplier += 0.1f;

                break;

            case 9:
                pInventory.itemLevels[9] += 1;

                PlayerStats.healthMultiplier += 0.15f;

                PlayerStats.maxHealth = PlayerStats.defaultHealth * PlayerStats.healthMultiplier;

                break;

            case 10:
                pInventory.itemLevels[10] += 1;

                PlayerStats.cooldownMultiplier -= 0.05f;

                break;

            default:
                break;
        }

        levelUpManager.levelUpWindow.SetActive(false);

        Time.timeScale = 1.0f;

        pInventory.hasItems[itemIndex] = true;

        PlayerStats.level += 1;

        for (int i = 0; i < PlayerStats.maxPassives; i++)
        {
            if (PlayerStats.ownedPassives[i] == itemIndex)
            {
                return;
            }

            if (PlayerStats.ownedPassives[i] == 0)
            {
                PlayerStats.ownedPassives[i] = itemIndex;

                PlayerStats.currentPassives += 1;
                Debug.Log("Current passives: " + PlayerStats.currentPassives);
                return;
            }
        }
    }

    public void LevelUpWeapon(int itemIndex)
    {
        switch (itemIndex)
        {
            case 1: //Bubble Wand
                switch (pInventory.itemLevels[1]) 
                {
                    case 0:
                        pInventory.itemLevels[1] += 1;

                        weapons[0].SetActive(true);

                        break;

                    case 1:
                        pInventory.itemLevels[1] += 1;

                        WeaponStats.bubbleProjectiles += 1;

                        break;

                    case 2:
                        pInventory.itemLevels[1] += 1;

                        WeaponStats.bubbleInterval *= 0.75f;

                        break;

                    case 3:
                        pInventory.itemLevels[1] += 1;

                        WeaponStats.bubbleDamage *= 1.25f;

                        break;

                    case 4:
                        pInventory.itemLevels[1] += 1;

                        WeaponStats.bubbleProjectiles += 2;

                        break;
                }

                break;

            case 2: //Explosion Staff
                switch (pInventory.itemLevels[2])
                {
                    case 0:
                        pInventory.itemLevels[2] += 1;

                        weapons[1].SetActive(true);

                        break;

                    case 1:
                        pInventory.itemLevels[2] += 1;

                        WeaponStats.explosionInterval = 6.0f;

                        break;

                    case 2:
                        pInventory.itemLevels[2] += 1;

                        WeaponStats.explosionSize = 1.25f;

                        break;

                    case 3:
                        pInventory.itemLevels[2] += 1;

                        WeaponStats.explosionDamage = 15.0f;

                        break;

                    case 4:
                        pInventory.itemLevels[2] += 1;

                        WeaponStats.explosionSize = 2.0f;

                        break;
                }

                break;

            case 3: //Wind Ring
                switch (pInventory.itemLevels[3])
                {
                    case 0:
                        pInventory.itemLevels[3] += 1;

                        weapons[2].SetActive(true);

                        break;

                    case 1:
                        pInventory.itemLevels[3] += 1;

                        WeaponStats.windRingInterval = 7.5f;

                        break;

                    case 2:
                        pInventory.itemLevels[3] += 1;

                        WeaponStats.windRingDamage = 6.0f;

                        break;

                    case 3:
                        pInventory.itemLevels[3] += 1;

                        WeaponStats.windRingSize = 1.5f;

                        break;

                    case 4:
                        pInventory.itemLevels[3] += 1;

                        WeaponStats.windRingInterval = 5.0f;

                        break;
                }

                break;

            case 4: //Unstable Potion
                switch (pInventory.itemLevels[4])
                {
                    case 0:
                        pInventory.itemLevels[4] += 1;

                        weapons[3].SetActive(true);

                        break;

                    case 1:
                        pInventory.itemLevels[4] += 1;

                        WeaponStats.unstablePotionBonusDamage = 5.0f;

                        break;

                    case 2:
                        pInventory.itemLevels[4] += 1;

                        WeaponStats.unstablePotionEffectDuration = 11.0f;

                        WeaponStats.unstablePotionBonusDamage = 6.0f;

                        break;

                    case 3:
                        pInventory.itemLevels[4] += 1;

                        WeaponStats.unstablePotionInterval = 6.0f;

                        WeaponStats.unstablePotionBonusDamage = 7.0f;

                        break;

                    case 4:
                        pInventory.itemLevels[4] += 1;

                        WeaponStats.unstablePotionEffectDuration = 16.0f;

                        WeaponStats.unstablePotionBonusDamage = 8.0f;

                        break;
                }

                break;

            case 5: //Bread
                switch (pInventory.itemLevels[5])
                {
                    case 0:
                        pInventory.itemLevels[5] += 1;

                        weapons[4].SetActive(true);

                        break;

                    case 1:
                        pInventory.itemLevels[5] += 1;

                        WeaponStats.breadDamage = 7.5f;

                        WeaponStats.breadSpeedMultiplier = 0.95f;

                        WeaponStats.geeseSize = 1.1f;

                        break;

                    case 2:
                        pInventory.itemLevels[5] += 1;

                        WeaponStats.maxGeese = 2;

                        WeaponStats.breadSpeedMultiplier = 0.9f;

                        WeaponStats.geeseSize = 1.2f;

                        break;

                    case 3:
                        pInventory.itemLevels[5] += 1;

                        WeaponStats.breadDamage = 10f;

                        WeaponStats.breadSpeedMultiplier = 0.85f;

                        WeaponStats.geeseSize = 1.3f;

                        break;

                    case 4:
                        pInventory.itemLevels[5] += 1;

                        WeaponStats.maxGeese = 4;

                        WeaponStats.breadSpeedMultiplier = 0.75f;

                        WeaponStats.geeseSize = 1.5f;

                        break;
                }

                break;
        }

        levelUpManager.levelUpWindow.SetActive(false);

        Time.timeScale = 1.0f;

        pInventory.hasItems[itemIndex] = true;

        PlayerStats.level += 1;

        for (int i = 0; i < PlayerStats.maxWeapons; i++)
        {
            if (PlayerStats.ownedWeapons[i] == itemIndex)
            {
                return;
            }

            if (PlayerStats.ownedWeapons[i] == 0)
            {
                PlayerStats.ownedWeapons[i] = itemIndex;

                PlayerStats.currentWeapons += 1;
                Debug.Log("Current weapons: " + PlayerStats.currentWeapons);
                return;
            }
        }
    }
}
