using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public ParticleSystem hitParticle;
    

    public float enemyHP;
    public float maxHP = 20f;

    public CinemachineSmoothPath path;
    public float smoothPos;
    public float speed = 0.5f;
    public Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        enemyHP = maxHP;
    }
    private void FixedUpdate()
    {
        smoothPos += speed * Time.deltaTime;
        if (smoothPos > path.MaxPos)
        {
            Destroy(gameObject);
            //SpawnManager.enemyCounter--;
        }
        rb.transform.position = path.EvaluateLocalPosition(smoothPos);
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
        GameManager.Instance.AddScore(100000);
        Destroy(gameObject);
    }
}
