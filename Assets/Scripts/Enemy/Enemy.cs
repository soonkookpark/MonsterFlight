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
        
        //�´� ����Ʈ
        //hitParticle.Stop();
        //hitParticle.Play();

        //���� �߰��ؾ���
        {

        }
        
        if (enemyHP <= 0)
        {
            OnDie();
        }
        
    }

    public void OnDie()
    {
        //�ı� ����
        //�ı� ����Ʈ
        Destroy(gameObject);
    }
}
