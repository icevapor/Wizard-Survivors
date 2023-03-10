using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayMenu;
    [SerializeField] private GameObject mainMenu;

    public void StartGame()
    {
        PlayerStats.level = 0;
        PlayerStats.maxLevel = 30;
        PlayerStats.experiencePoints = 0;
        PlayerStats.expToNextLevel = 10;

        PlayerStats.health = 30;
        PlayerStats.maxHealth = 30;
        PlayerStats.healthMultiplier = 1.0f;
        PlayerStats.healthRegen = 0.0025f;

        PlayerStats.damageMultiplier = 1.0f;
        PlayerStats.cooldownMultiplier = 1.0f;
        PlayerStats.movementSpeedMultiplier = 1.0f;

        PlayerStats.currentWeapons = 0;
        PlayerStats.maxWeapons = 3;
        PlayerStats.ownedWeapons = new int[3];
        PlayerStats.currentPassives = 0;
        PlayerStats.maxPassives = 3;
        PlayerStats.ownedPassives = new int[3];

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        howToPlayMenu.SetActive(true);

        mainMenu.SetActive(false);
    }

    public void Return()
    {
        howToPlayMenu.SetActive(false);

        mainMenu.SetActive(true);
    }
}
