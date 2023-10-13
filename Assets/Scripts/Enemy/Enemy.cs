using Cinemachine;
using System.Collections.Generic;
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
    public ParticleSystem explosionParticle;
    



    private void Awake()
    {
        monsterTable = new MonsterTable();
        monsterTable.Load();
        LoadData();
        rb = GetComponent<Rigidbody2D>();
    }

    void LoadData()
    {
        var allEnemyData = monsterTable.GetAllMonsterData();
        foreach (var data in allEnemyData)
        {
            monsterInfo[data.Mon_ID] = data;
        }
        foreach (var data in monsterInfo.Values)
        {
            Debug.Log(data.Mon_ID);
        }
    }

    private void Start()
    {
        enemyHP = maxHP;
        {
            //monsterTable = DataTableMgr.GetTable<MonsterTable>();

            // ��� ���� ������ ��������
            //var allMonsterData = monsterTable.GetAllMonsterData();
            //monsterInfo.ContainsKey()
            // ��� ���� �����͸� �ݺ��ؼ� ���
            //foreach (var data in allMonsterData)
            //{
            //    Debug.Log($"Monster ID: {data.Mon_ID}, HP: {data.Mon_HP}, Stage_HpuP:{data.Stage_Hpup}");
            //}
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
        foreach(var data in monsterInfo.Values)
        {
            Debug.Log(data.Mon_ID);
        }
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
