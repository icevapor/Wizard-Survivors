using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject player;
    Vector3 distanceToPlayer;

    void Start()
    {
        player = GameObject.Find("Wizard");
    }

    void Update()
    {
        distanceToPlayer = player.transform.position - transform.position;
        transform.Translate(distanceToPlayer.normalized * speed * Time.deltaTime);
    }

}
