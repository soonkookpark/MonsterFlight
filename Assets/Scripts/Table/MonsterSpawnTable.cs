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
        public int Root { get; set; }
        public int PatternType { get; set; }
        public int AppearTime { get; set; }
        public int Way { get; set; }
        public float StartPoint { get; set; }
        public string Mon_ID { get; set; }
        public int Amount { get; set; }
    }
    public Dictionary<int, Data> dic = new Dictionary<int, Data>();
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

    public Data GetSpawnData(int id)
    {
        if (!dic.ContainsKey(id))
        {
            return null;
        }
        return dic[id];
    }

    public List<Data> GetAllSpawnData()
    {
        return new List<Data>(dic.Values);
    }
}
