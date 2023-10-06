using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    //게임오브젝트로 이펙트 생성
    //public GameObject dieEffect;
    private int startingHealth = 102;
    private int playerhealth;
    
    private void OnEnable()
    {
        playerhealth = startingHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(playerhealth);
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet") || collision.CompareTag("ItemEnemy") || collision.CompareTag("BossEnemy"))
        {
            //GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
            //effect.getcomponent<dieEffect>().DieEffectOn();

            playerhealth--;
            //Debug.Log(playerhealth);
            if(playerhealth <= 0 )
            {
                gameObject.SetActive(false);
                GameManager.Instance.OnPlayerDead();
            }
        }
    }
}
