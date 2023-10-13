using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public class Data
    {
        //Root	PatternType	ApearTime	Way	StartPoint	MonsterID	Amount
        public int Root { get; set; }
        public int PatternType { get; set; }
        public int AppearTime { get; set; }
        public int Way { get; set; }
        public float StartPoint { get; set; }
        public string Mon_ID { get; set; }
        public int Amount { get; set; }
    }
    //public List<EnemyMovement> spawnMonster;
    //public GameObject[] enemies;
    public Enemy bossEnemy;
    public Enemy eliteEnemy;
    public Enemy ItemEnemy;
    public Enemy normalEnemy;
    public List<CinemachineSmoothPath> smoothPath;
    int currentRoot = 1;
    MonsterSpawnTable monsterSpawnTable;
    private Dictionary<int, MonsterSpawnTable.Data> spawnInfo = new Dictionary<int, MonsterSpawnTable.Data>();
    //private float spawnTimer = 0;
    //private float checkItemMonster; //아이템 주는 몬스터 체크
    //private float numberOfSpawn;//스폰할 수
    //private float spawnNumber;//스폰 한 수
    //private float TimeCheck;//현재 흐른 시간
    private int randNum = 0;
    public bool canSpawn = false;
    public int rootNum;
    int stageNum;
    int patternType;
    float spawnTime;
    //현재 시간
    float nowAddTime;


    private void Awake()
    {

        // MonsterSpawnTable 클래스의 인스턴스 생성
        monsterSpawnTable = new MonsterSpawnTable();
        // 데이터 로드
        monsterSpawnTable.Load();
        // 데이터를 spawnInfo 딕셔너리에 저장
        LoadData();


        //spawnInfo.Add()
    }
    void Start()
    {
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
        // 데이터를 받고
        // 지금 시간
        // 지금 타입
        //Debug.Log(spawnInfo[1]);
    }
    void Update()
    {
        
        foreach (var data in spawnInfo.Values)
        {
            //if(data.PatternType == patternType)
            {
                if (data.Root == currentRoot)
                {
                    //SpawnMonster(data.Way, data.StartPoint, data.Mon_ID, data.Amount);
                    currentRoot++;
                    Debug.Log(data.Way);
                    Debug.Log(data.StartPoint);
                    Debug.Log(data.Mon_ID);
                    Debug.Log(data.Amount);
                    if (spawnInfo.Count < data.Root)
                        currentRoot = 0;
                }
                   
            }
            

            //타입을 먼저 부여
            //patternType = data.Value.PatternType;
            //spawnTime = data.Value.AppearTime;
            nowAddTime += Time.deltaTime;
            if (stageNum % 2 == patternType && spawnTime >= nowAddTime) 
            {
                currentRoot = rootNum;
            }
            if (GameManager.Instance.IsGameOver)
            {
                Destroy(gameObject,30f);
            }
            {
            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    randNum = 0;
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    randNum = 1;
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha3))
            //{
            //    randNum = 2;
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha4))
            //{
            //    randNum = 3;
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha5))
            //{
            //    randNum = 4;
            //}
        }
        }
    }
    private IEnumerator SpawnEnemies()
    {
        {
            //while (!GameManager.Instance.IsGameOver)
            //{
            //    enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //    if (enemies.Length < 15)
            //    {
            //        if (Random.Range(0, 2) == 0)
            //        {
            //            Instantiate();
            //        }
            //        else
            //        {
            //            Instantiate(smoothPath[1]);
            //        }
            //    }
            //    bossEnemy = GameObject.FindGameObjectWithTag("BossEnemy");
            //    if (bossEnemy == null)
            //    {
            //        Instantiate(smoothPath[2]);
            //    }
            //    yield return new WaitForSeconds(5f);
            //}
        }
        while (!GameManager.Instance.IsGameOver && !canSpawn)
        {
            switch(randNum)
            {
                case 0:
                    SpawnItemMonster(randNum);
                    for (int i = 0; i < 2; i++)
                    {
                        yield return new WaitForSeconds(0.5f);
                        SpawnNormalMonster(randNum);
                    }
                    yield return new WaitForSeconds(2f);
                    randNum = 1;
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnNormalMonster(randNum);
                        yield return new WaitForSeconds(0.7f);
                    }
                    yield return new WaitForSeconds(2f);
                    randNum = 2;
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnNormalMonster(randNum);
                        SpawnNormalMonster(randNum+1);
                        yield return new WaitForSeconds(0.8f);
                    }
                    yield return new WaitForSeconds(2f);
                    randNum = 3;
                    break;
                case 3:
                    for (int i = 0; i < 5; i++)
                    {
                        SpawnNormalMonster(randNum+1);
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield return new WaitForSeconds(2f);
                    randNum = 4;
                    break;
                case 4:
                    for (int i = 0; i < 5; i++)
                    {
                        SpawnNormalMonster(randNum + 1);
                        SpawnNormalMonster(randNum + 2);
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield return new WaitForSeconds(2f);
                    randNum = 1;

                    break;
                case 5:
                    break;
                case 6:
                    break;
            }

        }
    }
    private void SpawnNormalMonster(int num)
    {
        var enemy = Instantiate(normalEnemy, smoothPath[num].m_Waypoints[0].position, Quaternion.identity);
        enemy.path = smoothPath[num];
    }
    private void SpawnItemMonster(int num)
    {
        var enemy = Instantiate(ItemEnemy, smoothPath[num].m_Waypoints[0].position, Quaternion.identity);
        enemy.path = smoothPath[num];
    }

    private void SpawnMonster(int way, float xPos, string ID, int num)
    {
        //var realStartPos = new Vector3(smoothPath[way].m_Waypoints[0].position.x + xPos, smoothPath[way].m_Waypoints[0].position.y);
        //var enemy = Instantiate(ID, realStartPos, Quaternion.identity);
        //enemy.path = smoothPath[way];
    }
    //private IEnumerator SpawnEnemies()
    //{

    //    switch(randNum)
    //    {
    //        case 0:
    //            SpawnItemPath();
    //            break;
    //    }

    //    yield return new WaitForSeconds(3f);
    //}

    //itemenemy 체크하는곳

}
