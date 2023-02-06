using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject weaponSelectMenu;
    [SerializeField] private GameObject[] weapons = new GameObject[5];
    [SerializeField] private ItemLevelUp itemLevelUp;
    private PlayerInventory pInventory;

    void Start()
    {
        Time.timeScale = 0.0f;
        pInventory = GameObject.Find("Wizard").GetComponent<PlayerInventory>();

        //This is to make sure that the WeaponStats static class gets reset every time a new run is started. Probably not the best practice but fuck it it works.
        WeaponStats.bubbleDamage = 5.0f;
        WeaponStats.bubbleInterval = 2.0f;
        WeaponStats.bubbleProjectiles = 1;

        WeaponStats.explosionDamage = 10.0f;
        WeaponStats.explosionInterval = 8.0f;
        WeaponStats.explosionSize = 1f;

        WeaponStats.windRingDamage = 3.0f;
        WeaponStats.windRingInterval = 10.0f;
        WeaponStats.windRingSize = 1f;

        WeaponStats.unstablePotionInterval = 9.0f;
        WeaponStats.unstablePotionEffectDuration = 7.0f;
        WeaponStats.unstablePotionBonusDamage = 2.5f;
        WeaponStats.unstablePotionBossDamage = 5.0f;

        WeaponStats.breadDamage = 5.0f;
        WeaponStats.breadSpeedMultiplier = 1.0f;
        WeaponStats.duckSize = 1.0f;
        WeaponStats.maxDucks = 1;
    }

    public void SelectBubbleWand()
    {
        itemLevelUp.LevelUpWeapon(1);

        Time.timeScale = 1.0f;

        weaponSelectMenu.SetActive(false);
    }

    public void SelectExplosionStaff()
    {
        itemLevelUp.LevelUpWeapon(2);

        Time.timeScale = 1.0f;

        weaponSelectMenu.SetActive(false);
    }

    public void SelectWindRing()
    {
        itemLevelUp.LevelUpWeapon(3);

        Time.timeScale = 1.0f;

        weaponSelectMenu.SetActive(false);
    }

    public void SelectUnstablePotion()
    {
        itemLevelUp.LevelUpWeapon(4);

        Time.timeScale = 1.0f;

        weaponSelectMenu.SetActive(false);
    }

    public void SelectBread()
    {
        itemLevelUp.LevelUpWeapon(5);

        Time.timeScale = 1.0f;

        weaponSelectMenu.SetActive(false);
    }
}
