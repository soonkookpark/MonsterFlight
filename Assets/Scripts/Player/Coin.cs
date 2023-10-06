using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{

    public void Use(GameObject go)
    {
        GameManager.Instance.AddScore(100);
    }
}
