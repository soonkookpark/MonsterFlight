using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public static ObjectManager instance;

    public List<GameObject> enemyAttack;
    public GameObject enemyBulletPrefab;
    private int amountBullet = 999;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        InitiailizedBullet();
    }
    private void InitiailizedBullet()
    {
        enemyAttack = new List<GameObject>();
        GameObject bullet;
        for (int i = 0; i < amountBullet; i++)
        {
            bullet = Instantiate(enemyBulletPrefab);
            bullet.SetActive(false);
            enemyAttack.Add(bullet);
        }
    }

    public GameObject GetEnemyBullet()
    {
        
        for(int i =0; i<amountBullet;i++)
        {
            if (!enemyAttack[i].activeInHierarchy)
            {
                return enemyAttack[i]; 
            }
        }
        return null;
    }

    public void OnDestroy()
    {
        enemyAttack = null;
    }
    public void ReturnEnemyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
    

}
