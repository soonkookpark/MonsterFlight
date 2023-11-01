using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public static ObjectManager instance;
    [Header("ObjectPool List")]
    private List<GameObject> enemyAttack = new List<GameObject>();
    [SerializeField]private GameObject enemyBulletPrefab;
    //private List<GameObject> item = new List<GameObject>();
    //[SerializeField] private GameObject coinItemPrefab;
    private List<GameObject> playerProjectile = new List<GameObject>();
    [SerializeField] private GameObject playerBulletPrefab;
    private int amountBullet = 500;
    //private int amountCoin = 10;
    private int amountPlayerProjectile = 100;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        InitiailizedObejct();
    }
    private void InitiailizedObejct()
    {
        GameObject bullet;
        //GameObject coinObject;
        GameObject playerProjectileObject;
        for (int i = 0; i < amountBullet; i++)
        {
            bullet = Instantiate(enemyBulletPrefab);
            bullet.SetActive(false);
            enemyAttack.Add(bullet);
        }
        //for(int i = 0; i < amountCoin ; i++)
        //{
        //    coinObject = Instantiate(coinItemPrefab);
        //    coinObject.SetActive(false);
        //    item.Add(coinObject);
        //}
        for(int i = 0; i < amountPlayerProjectile; i++)
        {
            playerProjectileObject = Instantiate(playerBulletPrefab);
            playerProjectileObject.SetActive(false);
            playerProjectile.Add(playerProjectileObject);
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
    public GameObject GetPlayerBullet()
    {

        for (int i = 0; i < amountPlayerProjectile; i++)
        {
            if (!playerProjectile[i].activeInHierarchy)
            {
                return playerProjectile[i];
            }
        }
        return null;
    }
    public void OnDestroy()
    {
        enemyAttack = null;
        playerProjectile = null;
    }
    public void ReturnEnemyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
    
    //오브젝트 풀에서 하나 빼내는 함수 만들기
}
