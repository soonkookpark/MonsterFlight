using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody2D enemyProjectile;
    private void Awake()
    {
        enemyProjectile = GetComponent<Rigidbody2D>();
    }
}
