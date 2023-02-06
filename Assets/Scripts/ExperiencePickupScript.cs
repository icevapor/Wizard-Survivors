using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickupScript : MonoBehaviour
{
    [SerializeField] private int expValue;
    [SerializeField] private float defaultPullInDistance;
    [SerializeField] private float pullInSpeed;
    private float pullInDistance;
    private Transform player;
    private Vector3 distanceToPlayer;

    void Start()
    {
        player = GameObject.Find("Wizard").transform;
    }

    void LateUpdate()
    {
        pullInDistance = defaultPullInDistance * (1 + (PlayerStats.level / 10));

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
