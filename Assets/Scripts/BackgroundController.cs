using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Collider2D selfCollider2D;
    private List<Collider2D> colliderList = new List<Collider2D>();
    private ContactFilter2D contactFilter;
    [SerializeField] private GameObject backgroundPrefab;
    private bool hasExtended;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ExtendBackground();
        }

        if (collider.gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }

    //Instantiates background sprites octodirectionally, ensuring the player is always on a background sprite.
    private void ExtendBackground()
    {
        if (!hasExtended )
        {
            Instantiate(backgroundPrefab, new Vector3(transform.position.x + 20.0f, transform.position.y, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x - 20.0f, transform.position.y, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x, transform.position.y + 20.0f, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x, transform.position.y - 20.0f, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x + 20.0f, transform.position.y + 20.0f, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x - 20.0f, transform.position.y + 20.0f, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x - 20.0f, transform.position.y - 20.0f, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x + 20.0f, transform.position.y - 20.0f, 0.0f), Quaternion.identity);
        }
        
        hasExtended = true;
    }

    void Start()
    {
        contactFilter = contactFilter.NoFilter();
        selfCollider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        //All this collider and contactfilter shit is so that any overlapping backgrounds are deleted. Easier than keeping track of every instantiated background in one cohesive grid.

        selfCollider2D.OverlapCollider(contactFilter, colliderList);
        if (colliderList.Count > 0)
        {
            foreach (Collider2D collider in colliderList)
            {
                if (collider.gameObject.CompareTag("Background"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }      
    }
}
