using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class MonsterSpawnTable : DataTable
{
    public class Data
    {
        //Root	PatternType	ApearTime	Way	StartPoint	MonsterID	Amount

        public string Root { get; set; }
        public int PatternType { get; set; }
        public int AppearTime { get; set; }
        public int Way { get; set; }
        public float StartPoint { get; set; }
        public int Mon_ID { get; set; }
        public int Amount { get; set; }
    }
    protected Dictionary<string, Data> dic = new Dictionary<string, Data>();
    public MonsterSpawnTable()
    {
        filePath = "Assets/Table/Resources/MonsterSpawnTable.csv";
        Load();
    }
    public override void Load()
    {
        string fileText = string.Empty;
        try
        {
            fileText = File.ReadAllText(filePath);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error Loading file:{e.Message}");
        }
        var csvStr = new TextAsset(fileText);

        using (TextReader reader = new StringReader(csvStr.text))
        {
            var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var records = csv.GetRecords<Data>();

            dic.Clear();
            foreach (var record in records)
            {
                dic.Add(record.Root, record);
            }
        }
    }

    public Data GetMonsterData(string id)
    {
        if (!dic.ContainsKey(id))
        {
            return null;
        }
        return dic[id];
    }

    public List<Data> GetAllMonsterData()
    {
        return new List<Data>(dic.Values);
    }
}
