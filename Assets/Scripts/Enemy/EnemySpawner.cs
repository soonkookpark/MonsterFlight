using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public static EnemySpawner instance;
    //public List<EnemyMovement> spawnMonster;
    [SerializeField]private GameObject[] Backgrounds;
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
    float speed;
    private bool bossDeath = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        monsterSpawnTable = new MonsterSpawnTable();
        // 데이터 로드
        monsterSpawnTable.Load();
        ResetSpawner();
        // 데이터를 spawnInfo 딕셔너리에 저장
        //LoadData();
    }
    public void ResetSpawner()
    {
        currentRoot = 1;
        nowAddTime = 0;
        amount = 0;
        speed = 0;

        LoadData(); // 데이터를 다시 로드합니다.
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
            if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentRoot = 120;
            }
            nowAddTime += Time.deltaTime;
            //만약 테이블을 다 순회했을 경우
            if (spawnInfo.Count < currentRoot)
            {
                currentRoot = 1;
                nowAddTime = 0;
            }
            //맞는 패턴타입을 찾고
            if (spawnInfo[currentRoot].PatternType%2 == (stageNum % 2))
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
                    speed = monData.Speed;
                    StartCoroutine(SpawnMonster(courseNum, startPos, monsterID, amount, speed, stageNum));
                    if(nowAddTime < 150)
                        currentRoot++;
                    Debug.Log(currentRoot);
                    yield return null;
                }
                
                //시간을 측정하고
                //몬스터가 다 나가면, 루트를 하나 올리고
            }
            else
            {
                //Debug.Log(currentRoot);
            }
            if(nowAddTime>= 150)
            {
                BossPattern();
            }
            //보스전에선 저기가 끝날때까지
            //patterntype1번이 끝나면 2번이 온다.
            //1번의 보스

            //보스 시간이면 currentRoot 를 건너뛰어야함.
            //보스 끝나면 다시 더하고
            // 최고기록 저장하고


            //만약 패턴타입이 다른경우 루트를 더해서 맞는 패턴타입을 찾는다.

            //currentRoot++;
            CheckTable();
            yield return null;
            //다르면 번호를 더하고
        }
    }
    private IEnumerator SpawnMonster(int way, float xPos, string ID, int num,float speed, int stageInfo)
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
            spawnEnemy.SetHp(stageInfo);
            //Debug.Log(spawnEnemy.enemyHP);
            yield return new WaitForSeconds(0.5f);
        }
        //나중에 스케일로 난이도 조정할 수 있는 기능 추가
    }
    private void CheckTable()
    {
        if (spawnInfo.Count <= currentRoot)
        {
            currentRoot = 1;
            nowAddTime = 0;
        }
    }
    private void BossPattern()
    {
        var Boss = GameObject.FindGameObjectWithTag("BossEnemy");
        if (Boss == null&&bossDeath)
        {
            Debug.Log("보스죽음.");
            currentRoot++;
            stageNum++;
            nowAddTime = 0;
            switch (stageNum%3)
            {
                case 1:
                    Backgrounds[2].SetActive(false);
                    Backgrounds[0].SetActive(true);
                    break;
                case 2:
                    Backgrounds[0].SetActive(false);
                    Backgrounds[1].SetActive(true);
                    break;
                case 0:
                    Backgrounds[1].SetActive(false);
                    Backgrounds[2].SetActive(true);
                    break;
            }
            bossDeath = false;
        }
        else
        {
            bossDeath = true;
            Debug.Log("보스살음.");
        }
    }
}
