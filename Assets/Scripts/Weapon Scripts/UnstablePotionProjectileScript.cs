using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePotionProjectileScript : MonoBehaviour
{
    [SerializeField] private GameObject unstablePotionPool;
    private Rigidbody2D rb;
    private Transform player;
    [SerializeField] private float throwForce;
    private float yGoalPost;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Wizard").GetComponent<Transform>();
        yGoalPost = player.position.y + Random.Range(-1.0f, 0.0f);
        ThrowBottle();
    }

    void Update()
    {
        if (transform.position.y <= yGoalPost)
        {
            rb.Sleep();
            unstablePotionPool.SetActive(true);
        }
    }

    //Generates random vector used for throwing the potion up at an angle. Then adds an impulse force to the bottle with that vector as a direction.
    private void ThrowBottle()
    {
        Vector2 randomVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 1.0f));
        rb.AddForce(randomVector.normalized * throwForce, ForceMode2D.Impulse);
    }
}
