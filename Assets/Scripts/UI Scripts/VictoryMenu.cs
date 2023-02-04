using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private SpawnManager spawnManager;
    private PuppetController puppetCon;
    private bool puppetFound;

    void Update()
    {
        if (spawnManager.bossActive && !puppetFound)
        {
            puppetCon = GameObject.Find("Puppet").GetComponent<PuppetController>();
        }

        if (puppetCon != null)
        {
            puppetFound = true;
        }

        if (puppetFound == true && puppetCon == null)
        {
            victoryMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
