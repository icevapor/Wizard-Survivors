using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public string[] itemNames = new string[12];
    public bool[] hasItems = new bool[12];
    public int[] itemLevels = new int[12];

    void Start()
    {
        itemNames = new string[12];
        hasItems = new bool[12];
        itemLevels = new int[12];
    }
}
