using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite damagedSprite;
    private Rigidbody2D rb;
    [SerializeField] private float defaultSpeed;
    private float speed;
    private bool damageCooldown;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RegenerateHealth());
    }

    void Update()
    {
        speed = defaultSpeed * PlayerStats.movementSpeedMultiplier;

        //Moves player based on wasd input.
        Vector2 moveVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;

            moveVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;

            moveVector.x += 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveVector.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector.y -= 1;
        }

        rb.velocity = moveVector.normalized * speed;
    }

    public void Damage(float damage)
    {
        if (!damageCooldown)
        {
            PlayerStats.health -= damage;

            StartCoroutine(Damaged());
        }

        Debug.Log(PlayerStats.health);
    }

    private IEnumerator Damaged()
    {
        damageCooldown = true;

        Sprite normalSprite = spriteRenderer.sprite;

        spriteRenderer.sprite = damagedSprite;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.sprite = normalSprite;

        damageCooldown = false;
    }

    private IEnumerator RegenerateHealth()
    {
        while (GameManager.gameActive)
        {
            yield return new WaitForSeconds(1.0f);

            float regenAmount = PlayerStats.maxHealth * PlayerStats.healthRegen;

            if (PlayerStats.health + regenAmount <= PlayerStats.maxHealth)
            {
                PlayerStats.health += regenAmount;
            }

            else
            {
                PlayerStats.health = PlayerStats.maxHealth;
            }
        }
    }

}
