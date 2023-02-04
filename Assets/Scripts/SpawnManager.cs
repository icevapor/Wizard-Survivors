using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    private int minEnemyIndex = 0;
    private int maxEnemyIndex = 0;
    public bool bossActive;


    private GameObject player;
    [SerializeField] private int maxEnemies;
    public int currentEnemies;
    private List<GameObject> aliveEnemies = new List<GameObject>();
    private float spawnDistance = 4.0f;

    void Start()
    {
        player = GameObject.Find("Wizard");
        StartCoroutine(IncreaseDifficulty());
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {      
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnBoss(enemyPrefabs[3]);

            maxEnemies = 0;
        }        

        if (!bossActive)
        {
            for (int i = 0; i < aliveEnemies.Count; i++)
            {
                if (aliveEnemies[i] == null)
                {
                    aliveEnemies.RemoveAt(i);

                    aliveEnemies.TrimExcess();
                }
            }

            if (aliveEnemies.Count > maxEnemies)
            {
                Destroy(aliveEnemies[maxEnemies - 1]);

                aliveEnemies.RemoveAt(maxEnemies - 1);
            }
        }      
    }

    //Generates random spawn location near but just offscreen of player.
    private IEnumerator SpawnEnemy()
    {
        while (GameManager.gameActive)
        {
            if (currentEnemies < maxEnemies)
            {
                GameObject enemy = enemyPrefabs[Random.Range(minEnemyIndex, maxEnemyIndex + 1)];
                //Determines what direction the enemy spawns from. 0 is the left of the screen, 1 is the right of the screen, 2 is above the screen, 3 is below the screen.
                int spawnQuadrant = Random.Range(0, 4);

                Vector3 spawnLocation = new Vector3(0.0f, 0.0f, 0.0f);

                switch (spawnQuadrant)
                {
                    case 0:
                        spawnLocation += new Vector3(player.transform.position.x - spawnDistance, player.transform.position.y + Random.Range(-spawnDistance, spawnDistance), 0.0f);
                        break;
                    case 1:
                        spawnLocation += new Vector3(player.transform.position.x + spawnDistance, player.transform.position.y + Random.Range(-spawnDistance, spawnDistance), 0.0f);
                        break;
                    case 2:
                        spawnLocation += new Vector3(player.transform.position.x + Random.Range(-spawnDistance, spawnDistance), player.transform.position.y + spawnDistance, 0.0f);
                        break;
                    case 3:
                        spawnLocation += new Vector3(player.transform.position.x + Random.Range(-spawnDistance, spawnDistance), player.transform.position.y - spawnDistance, 0.0f);
                        break;
                }

                GameObject newEnemy = Instantiate(enemy, spawnLocation, Quaternion.identity);

                newEnemy.name = GenerateEnemyName(newEnemy.name);

                currentEnemies += 1;

                aliveEnemies.Add(newEnemy);

                aliveEnemies.TrimExcess();
            }

            yield return new WaitForSeconds(0.1f);
        }      
    }

    void SpawnBoss(GameObject enemy)
    {
        bossActive = true;

        foreach (GameObject aliveEnemy in aliveEnemies)
        {
            Destroy(aliveEnemy);
        }

        aliveEnemies.Clear();

        //Determines what direction the enemy spawns from. 0 is the left of the screen, 1 is the right of the screen, 2 is above the screen, 3 is below the screen.
        int spawnQuadrant = Random.Range(0, 4);

        Vector3 spawnLocation = new Vector3(0.0f, 0.0f, 0.0f);

        switch (spawnQuadrant)
        {
            case 0:
                spawnLocation += new Vector3(player.transform.position.x - spawnDistance, player.transform.position.y + Random.Range(-spawnDistance, spawnDistance), 0.0f);
                break;
            case 1:
                spawnLocation += new Vector3(player.transform.position.x + spawnDistance, player.transform.position.y + Random.Range(-spawnDistance, spawnDistance), 0.0f);
                break;
            case 2:
                spawnLocation += new Vector3(player.transform.position.x + Random.Range(-spawnDistance, spawnDistance), player.transform.position.y + spawnDistance, 0.0f);
                break;
            case 3:
                spawnLocation += new Vector3(player.transform.position.x + Random.Range(-spawnDistance, spawnDistance), player.transform.position.y - spawnDistance, 0.0f);
                break;
        }

        GameObject newEnemy = Instantiate(enemy, spawnLocation, Quaternion.identity);
        newEnemy.name = "Puppet";
    }

    private IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(45.0f);

        maxEnemies = 10;

        yield return new WaitForSeconds(45.0f);

        maxEnemies = 11;

        yield return new WaitForSeconds(45.0f);

        maxEnemies = 12;

        maxEnemyIndex = 1;

        yield return new WaitForSeconds(45.0f);

        maxEnemies = 13;

        yield return new WaitForSeconds(45.0f);

        maxEnemies = 14;

        yield return new WaitForSeconds(45.0f);

        maxEnemies = 15;

        minEnemyIndex = 1;

        yield return new WaitForSeconds(45.0f);

        maxEnemies = 16;

        maxEnemyIndex = 2;

        yield return new WaitForSeconds(120.0f);

        minEnemyIndex = 2;

        yield return new WaitForSeconds(60.0f);

        maxEnemies = 0;

        yield return new WaitForSeconds(5.0f);

        SpawnBoss(enemyPrefabs[3]);
    }

    //This method is used to basically assign ids to each cloned enemy.
    //This is used to keep track of enemies for various reasons, such as making sure the same enemy isn't damaged more than once during a lingering explosion.
    private string GenerateEnemyName(string name)
    {
        return name + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10);
    }
}
