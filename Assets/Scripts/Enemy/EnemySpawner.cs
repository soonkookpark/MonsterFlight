using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
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
    //현재 시간
    float nowAddTime;
    string monsterID;
    int amount;
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
        
    }
    void Update()
    {
        //while (!GameManager.Instance.IsGameOver && !canSpawn)
        
        //맞는 패턴타입을 찾고
        //해당패턴타입의 첫 루트를 가져온다.
        //시간을 측정하고
        //몬스터가 다 나가면, 루트를 하나 올리고
        //if (spawnInfo[currentRoot].PatternType == (patternType % 2))
        //{
        //    currentRoot++;
        //    if (spawnInfo.Count <= currentRoot)
        //    {
        //        currentRoot = 1;
        //    }
        //    if (spawnInfo[currentRoot].PatternType == (patternType % 2))
        //    {
        //        var monData = spawnInfo[currentRoot];
        //        if (monData.AppearTime < nowAddTime)

        //            monsterID = monData.Mon_ID; //소환할 몬스터 아이디
        //        spawnTime = monData.AppearTime; //출현 시간
        //        courseNum = monData.Way;
        //        startPos = monData.StartPoint;
        //        amount = monData.Amount;





        //    }
        //    else
        //    {
        //        patternType++;
        //    }


        //}


        ////타입을 먼저 부여
        ////patternType = data.Value.PatternType;
        ////spawnTime = data.Value.AppearTime;
        
        //if (stageNum % 2 == patternType && spawnTime >= nowAddTime)
        //{
        //Debug.Log(nowAddTime);
        //}
        if (GameManager.Instance.IsGameOver)
        {
            Destroy(gameObject, 30f);
        }
        
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            nowAddTime += Time.deltaTime;
            //만약 테이블을 다 순회했을 경우
            if (spawnInfo.Count <= currentRoot)
            {
                currentRoot = 1;
            }
            //맞는 패턴타입을 찾고
            if (spawnInfo[currentRoot].PatternType == (stageNum % 2))
            {
                //해당패턴타입의 첫 루트를 가져온다.
                var monData = spawnInfo[currentRoot];
                //소환할 시간이 된다면 능력치를 할당함.
                if (monData.AppearTime < nowAddTime)
                {
                    spawnTime = nowAddTime;
                    monsterID = monData.Mon_ID; //소환할 몬스터 아이디
                                                //spawnTime = monData.AppearTime; //출현 시간
                    courseNum = monData.Way;
                    startPos = monData.StartPoint;
                    amount = monData.Amount;
                    SpawnMonster(courseNum, startPos, monsterID, amount);
                    currentRoot++;
                    yield return null;
                }
                //시간을 측정하고
                //몬스터가 다 나가면, 루트를 하나 올리고
            }
            else
            {
                currentRoot++;
            }
            //만약 패턴타입이 다른경우 루트를 더해서 맞는 패턴타입을 찾는다.

            //currentRoot++;
            CheckTable();
            yield return null;
            //다르면 번호를 더하고
        }
    }
    private void SpawnMonster(int way, float xPos, string ID, int num)
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
        var spawnEnemy = Instantiate(reallySpawnEnemy, realStartPos, Quaternion.identity);
        //spawnEnemy.GetComponent<Enemy>().InitializePath(smoothPath[way], realStartPos);
        spawnEnemy.path = smoothPath[way];
        spawnEnemy.SetMonsterID(ID);
        //나중에 스케일로 난이도 조정할 수 있는 기능 추가
    }
    private void CheckTable()
    {
        if (spawnInfo.Count < currentRoot)
            currentRoot = 0;
    }
    private void SpawnNormalMonster(int num)
    {
        //var enemy = Instantiate(normalEnemy, smoothPath[num].m_Waypoints[0].position, Quaternion.identity);
        //enemy.path = smoothPath[num];
        //enemy.SetMonsterID(monsterID);
    }
    private void SpawnItemMonster(int num)
    {
        //var enemy = Instantiate(ItemEnemy, smoothPath[num].m_Waypoints[0].position, Quaternion.identity);
        //enemy.path = smoothPath[num];
       // enemy.SetMonsterID(monsterID);
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
