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
