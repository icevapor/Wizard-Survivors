using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private float baseScale;

    void Update()
    {
        transform.localScale = new Vector3((PlayerStats.health / PlayerStats.maxHealth) * baseScale, 0.03f, 1f);
        transform.localPosition = new Vector3(0f + (-0.15f * ((PlayerStats.maxHealth - PlayerStats.health)/ PlayerStats.maxHealth)), 0.3f, 0f);
    }
}
