using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public ParticleSystem hitParticle;
    MonsterTable monsterTable;
    Dictionary<string, MonsterTable.Data> monsterInfo = new Dictionary<string, MonsterTable.Data>();
    public float enemyHP;
    public float maxHP = 20f;

    public CinemachineSmoothPath path;
    public float smoothPos;
    public float speed = 0.5f;
    public Rigidbody2D rb;
    private bool isDead = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        enemyHP = maxHP;
        monsterTable = DataTableMgr.GetTable<MonsterTable>();

        // 모든 몬스터 데이터 가져오기
        var allMonsterData = monsterTable.GetAllMonsterData();

        // 모든 몬스터 데이터를 반복해서 사용
        foreach (var data in allMonsterData)
        {
            Debug.Log($"Monster ID: {data.Mon_ID}, HP: {data.Mon_HP}, Stage_HpuP:{data.Stage_Hpup}");
        }
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
        
        //맞는 이펙트
        //hitParticle.Stop();
        //hitParticle.Play();

        //사운드 추가해야함
        {

        }

        if (enemyHP <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                OnDie();
            }
        }

    }

    public void OnDie()
    {
        //파괴 사운드
        //파괴 이펙트
        
        GameManager.Instance.AddScore(100000);
        Destroy(gameObject);
    }
}
