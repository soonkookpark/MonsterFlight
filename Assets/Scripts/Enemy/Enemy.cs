using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public ParticleSystem hitParticle;

    public float enemyHP;
    public float maxHP = 5f;
    private void Start()
    {
        enemyHP = maxHP;
    }
    private void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        Mathf.Clamp(enemyHP -= damage, 0, maxHP);
        
        //맞는 이펙트
        //hitParticle.Stop();
        //hitParticle.Play();

        //사운드 추가해야함
        {

        }
        
        if (enemyHP <= 0)
        {
            OnDie();
        }
        
    }

    public void OnDie()
    {
        //파괴 사운드
        //파괴 이펙트
        Destroy(gameObject);
    }
}
