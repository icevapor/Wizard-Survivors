using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public string[] itemNames = new string[11];
    public bool[] hasItems = new bool[11];
    public int[] itemLevels = new int[11];

    void Start()
    {
        itemNames = new string[11];
        hasItems = new bool[11];
        itemLevels = new int[11];
    }
}
