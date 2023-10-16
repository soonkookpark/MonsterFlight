using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    //public List<EnemyMovement> spawnMonster;
    //public GameObject[] enemies;
    public List<Enemy> enemy;
    private Enemy reallySpawnEnemy;
    //public Enemy eliteEnemy;
    //public Enemy ItemEnemy;
    //public Enemy normalEnemy;
    public List<CinemachineSmoothPath> smoothPath;
    int currentRoot = 1;
    MonsterSpawnTable monsterSpawnTable;
    private Dictionary<int, MonsterSpawnTable.Data> spawnInfo = new Dictionary<int, MonsterSpawnTable.Data>();
    private int randNum = 0;
    public bool canSpawn = false;
    int stageNum=1;
    int courseNum = 1;
    //int patternType=1;
    float startPos;
    float spawnTime;
    //float mag;
    //���� �ð�
    float nowAddTime;
    string monsterID;
    int amount;
    float speed;
    private void Awake()
    {
        // MonsterSpawnTable Ŭ������ �ν��Ͻ� ����
        if (instance != null && instance != this)
        {
            // �̹� �ٸ� EnemySpawner�� �����ϸ� ���� ��ũ��Ʈ�� �ı�
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        monsterSpawnTable = new MonsterSpawnTable();
        // ������ �ε�
        monsterSpawnTable.Load();
        // �����͸� spawnInfo ��ųʸ��� ����
        //LoadData();
    }
    void Start()
    {
        nowAddTime = 0;
        amount = 0;
        speed = 0;
        currentRoot = 1;
        LoadData();

        StartCoroutine(SpawnEnemies());
    }
    void LoadData()
    {
        var allSpawnData = monsterSpawnTable.GetAllSpawnData();

        foreach (var data in allSpawnData)
        {
            spawnInfo[data.Root] = data;
        }
        
    }
    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            //Destroy(gameObject, 30f);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            nowAddTime += Time.deltaTime;
            //���� ���̺��� �� ��ȸ���� ���
            if (spawnInfo.Count <= currentRoot)
            {
                currentRoot = 1;
                nowAddTime = 0;
            }
            //�´� ����Ÿ���� ã��
            if (spawnInfo[currentRoot].PatternType == (stageNum % 2))
            {
                //�ش�����Ÿ���� ù ��Ʈ�� �����´�.
                var monData = spawnInfo[currentRoot];
                //��ȯ�� �ð��� �ȴٸ� �ɷ�ġ�� �Ҵ���.
                if (monData.AppearTime < nowAddTime)
                {
                    spawnTime = nowAddTime;
                    monsterID = monData.Mon_ID; //��ȯ�� ���� ���̵�
                                                //spawnTime = monData.AppearTime; //���� �ð�
                    courseNum = monData.Way;
                    startPos = monData.StartPoint;
                    amount = monData.Amount;
                    speed = monData.Speed;
                    StartCoroutine(SpawnMonster(courseNum, startPos, monsterID, amount, speed));
                    currentRoot++;
                    yield return null;
                }
                //�ð��� �����ϰ�
                //���Ͱ� �� ������, ��Ʈ�� �ϳ� �ø���
            }
            else
            {
                currentRoot++;
            }
            //���� ����Ÿ���� �ٸ���� ��Ʈ�� ���ؼ� �´� ����Ÿ���� ã�´�.

            //currentRoot++;
            CheckTable();
            yield return null;
            //�ٸ��� ��ȣ�� ���ϰ�
        }
    }
    private IEnumerator SpawnMonster(int way, float xPos, string ID, int num,float speed)
    {

        foreach(var e in enemy)
        {
            if(e.gameObject.name==ID)
            {
                reallySpawnEnemy = e;
                break;
            }
        }
        var realStartPos = new Vector3(smoothPath[way].m_Waypoints[0].position.x + xPos, smoothPath[way].m_Waypoints[0].position.y);
        for(int i = 0; i < num; i++) 
        { 
            var spawnEnemy = Instantiate(reallySpawnEnemy, realStartPos, Quaternion.identity);
            //spawnEnemy.GetComponent<Enemy>().InitializePath(smoothPath[way], realStartPos);
            spawnEnemy.path = smoothPath[way];
            spawnEnemy.SetMonsterID(ID);
            spawnEnemy.SetSpeed(speed);
            yield return new WaitForSeconds(0.5f);
        }
        //���߿� �����Ϸ� ���̵� ������ �� �ִ� ��� �߰�
    }
    private void CheckTable()
    {
        if (spawnInfo.Count < currentRoot)
        {
            currentRoot = 1;
            nowAddTime = 0;
        }
    }
}
