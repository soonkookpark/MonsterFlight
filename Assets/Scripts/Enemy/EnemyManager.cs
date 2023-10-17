using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    MonsterTable monsterTable;
    Dictionary<string, MonsterTable.Data> monsterInfo = new Dictionary<string, MonsterTable.Data>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            //Destroy(this.gameObject);
            //return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);

        LoadEnemyData();
    }

    private void LoadEnemyData()
    {
        monsterTable = new MonsterTable();
        monsterTable.Load();
        LoadData();
    }
    private void LoadData()
    {
        monsterInfo.Clear();
        var allEnemyData = monsterTable.GetAllMonsterData();
        foreach (var data in allEnemyData)
        {
            monsterInfo[data.Mon_ID] = data;
        }
    }
    public MonsterTable.Data GetMonsterData(string monsterID)
    {
        if (monsterInfo.ContainsKey(monsterID))
        {
            return monsterInfo[monsterID];
        }
        else
        {
            Debug.LogError("몬스터 데이터를 찾을 수 없습니다.");
            return null;
        }
    }

}
