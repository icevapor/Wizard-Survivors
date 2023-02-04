using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuParent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;

            menuParent.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        menuParent.SetActive(false);

        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(0);
    }

}
