using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float time;
    private int seconds;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        time += Time.deltaTime;

        seconds = Mathf.RoundToInt(time);

        if (seconds % 60 < 10)
        {
            timerText.text = (seconds / 60) + ":0" + (seconds % 60);
        }

        else
        {
            timerText.text = (seconds / 60) + ":" + (seconds % 60);
        }     
    }

}
