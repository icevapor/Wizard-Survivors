using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Collider2D selfCollider2D;
    private List<Collider2D> colliderList = new List<Collider2D>();
    private ContactFilter2D contactFilter;
    [SerializeField] private GameObject backgroundPrefab;
    private Transform backgroundParent;
    private bool hasExtended;
    private float backgroundOffset = 7.5f;

    void Start()
    {
        contactFilter = contactFilter.NoFilter();
        selfCollider2D = GetComponent<Collider2D>();
        backgroundParent = GameObject.Find("Background").transform;
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
            GameObject background;

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x + backgroundOffset, transform.position.y, 0.0f);

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x - backgroundOffset, transform.position.y, 0.0f);

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x, transform.position.y + backgroundOffset, 0.0f);

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x, transform.position.y - backgroundOffset, 0.0f);

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x + backgroundOffset, transform.position.y + backgroundOffset, 0.0f);

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x + backgroundOffset, transform.position.y + backgroundOffset, 0.0f);

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x - backgroundOffset, transform.position.y + backgroundOffset, 0.0f);

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x - backgroundOffset, transform.position.y - backgroundOffset, 0.0f);

            background = Instantiate(backgroundPrefab, backgroundParent);
            background.transform.position = new Vector3(transform.position.x + backgroundOffset, transform.position.y - backgroundOffset, 0.0f);

            /*
            Instantiate(backgroundPrefab, new Vector3(transform.position.x - backgroundOffset, transform.position.y, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x, transform.position.y + backgroundOffset, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x, transform.position.y - backgroundOffset, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x + backgroundOffset, transform.position.y + backgroundOffset, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x - backgroundOffset, transform.position.y + backgroundOffset, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x - backgroundOffset, transform.position.y - backgroundOffset, 0.0f), Quaternion.identity);
            Instantiate(backgroundPrefab, new Vector3(transform.position.x + backgroundOffset, transform.position.y - backgroundOffset, 0.0f), Quaternion.identity);*/
        }
        
        hasExtended = true;
    }

    
}
