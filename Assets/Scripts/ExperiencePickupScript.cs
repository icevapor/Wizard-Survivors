using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickupScript : MonoBehaviour
{
    [SerializeField] private int expValue;
    [SerializeField] private float pullInDistance;
    [SerializeField] private float pullInSpeed;
    private Transform player;
    private Vector3 distanceToPlayer;

    void Start()
    {
        player = GameObject.Find("Wizard").transform;
    }

    void LateUpdate()
    {
        distanceToPlayer = player.position - transform.position;

        if (distanceToPlayer.magnitude < pullInDistance)
        {
            transform.Translate(distanceToPlayer.normalized * pullInSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (PlayerStats.level < PlayerStats.maxLevel)
        {
            PlayerStats.experiencePoints += expValue;
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
