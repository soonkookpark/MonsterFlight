using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;

    public Transform playerTransform;

    private void Update()
    {
        //if()
        //GameObject item = Instantiate()
    }
    private void MakeItem(int num)
    {
        GameObject item = Instantiate(items[num]);
    }
}
