using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetProjectile : MonoBehaviour
{
    [SerializeField] private Transform[] projectilePointers = new Transform[5]; //0 is projectile origin that needs to be rotated, 1 - 5 are pointers used for giving projectiles velocity.
    private Rigidbody2D rb;
    public int identity;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileSpeed;
    private Vector3 projectileTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        for (int i = 0; i < 5; i++)
        {
            string pointerName = "ProjectilePointer" + i;
            projectilePointers[i] = GameObject.Find(pointerName).transform;
        }

        projectileTarget = projectilePointers[identity].position - transform.position;

        rb.AddForce(projectileTarget.normalized * projectileSpeed, ForceMode2D.Impulse);

        StartCoroutine(ProjectileLife());
    }

    void Update()
    {
        transform.localScale *= (1f + (0.25f * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        damageable?.Damage(projectileDamage);

        Destroy(gameObject);
    }

    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(6.0f);

        Destroy(gameObject);
    }


}
