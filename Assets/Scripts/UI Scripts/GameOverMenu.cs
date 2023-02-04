using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuParent;

    void Update()
    {
        if (PlayerStats.health <= 0)
        {
            Time.timeScale = 0.0f;

            menuParent.SetActive(true);
        }
    }
}
