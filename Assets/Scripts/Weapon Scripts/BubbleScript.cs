using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float projectileForce;
    private Vector2 distanceToTarget;

    private Transform player;
    private Vector2 pointA;
    private Vector2 pointB;

    [SerializeField] private LayerMask layerMask;
    private GameObject target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Wizard").GetComponent<Transform>();
        pointA = new Vector2(player.position.x - 10.0f, player.position.y + 10.0f);
        pointB = new Vector2(player.position.x + 10.0f, player.position.y - 10.0f);

        target = Physics2D.OverlapArea(pointA, pointB, layerMask).gameObject;
        distanceToTarget = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);

        rb.AddForce(distanceToTarget * projectileForce, ForceMode2D.Impulse);
    }
}
