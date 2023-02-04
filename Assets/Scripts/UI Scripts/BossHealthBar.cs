using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private float baseScale;
    [SerializeField] private GameObject redBar;
    [SerializeField] private GameObject barOutline;
    private PuppetController puppetCon;

    void Update()
    {
        if (puppetCon != null)
        {   
            redBar.transform.localScale = new Vector3((puppetCon.health / puppetCon.maxHealth) * baseScale, 8f, 1f);
            redBar.transform.localPosition = new Vector3(0f + (-612f * ((puppetCon.maxHealth - puppetCon.health) / puppetCon.maxHealth)), -450f, 0f);
        }
    }

    public void HealthBarOn()
    {
        redBar.SetActive(true);
        barOutline.SetActive(true);
        puppetCon = GameObject.Find("Puppet").GetComponent<PuppetController>();
    }

    public void HealthBarOff()
    {
        puppetCon = null;
        redBar.SetActive(false);
        barOutline.SetActive(false);
    }
}
