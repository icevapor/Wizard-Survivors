using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    private GameObject player;
    [SerializeField] private int maxEnemies;
    public int currentEnemies;

    void Start()
    {
        player = GameObject.Find("Wizard");
    }

    void Update()
    {
        if (currentEnemies < maxEnemies)
        {
            SpawnEnemy(enemyPrefabs[0]);
        }
    }

    //Generates random spawn location near but just offscreen of player.
    void SpawnEnemy(GameObject enemy)
    {
        //Determines what direction the enemy spawns from. 0 is the left of the screen, 1 is the right of the screen, 2 is above the screen, 3 is below the screen.
        int spawnQuadrant = Random.Range(0, 4);

        Vector3 spawnLocation = new Vector3(0.0f, 0.0f, 0.0f);

        switch (spawnQuadrant)
        {
            case 0:
                spawnLocation += new Vector3(player.transform.position.x - 15.0f, player.transform.position.y + Random.Range(-15.0f, 15.0f), 0.0f);
                break;
            case 1:
                spawnLocation += new Vector3(player.transform.position.x + 15.0f, player.transform.position.y + Random.Range(-15.0f, 15.0f), 0.0f);
                break;
            case 2:
                spawnLocation += new Vector3(player.transform.position.x + Random.Range(-15.0f, 15.0f), player.transform.position.y + 15.0f, 0.0f);
                break;
            case 3:
                spawnLocation += new Vector3(player.transform.position.x + Random.Range(-15.0f, 15.0f), player.transform.position.y - 15.0f, 0.0f);
                break;
        }      

        Instantiate(enemy, spawnLocation, Quaternion.identity);

        currentEnemies += 1;
    }
}
