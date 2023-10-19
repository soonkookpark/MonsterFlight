using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Enumeration;
using UnityEngine;

public class MonsterTable : DataTable
{
    //private string path = "MonsterTable.csv";
    private string path = "MonsterTable";

    public class Data
    {
        public string Mon_ID { get; set; }
        public string Mon_Name { get; set; }
        public int Mon_HP { get; set; }
        public int Mon_ATK { get; set; }
        //public int ATKNUM { get; set; }
        public int Stage_Hpup { get; set; }
        public int Type { get; set; }
        public int Mon_Score { get; set; }
        //public int STRING { get; set; }

    }
    public Dictionary<string, Data> dic = new Dictionary<string, Data>();

    public MonsterTable()
    {
        //filePath = Path.Combine(Application.streamingAssetsPath, path);
        filePath = path;
        //Debug.Log(filePath);
        Load();
    }

    public override void Load()
    {

        //string fileText = string.Empty;
        //try
        //{
        //    fileText = File.ReadAllText(filePath);
        //}
        //catch (Exception e)
        //{
        //    Debug.LogError($"Error Loading file:{e.Message}");
        //}
        //var csvStr = new TextAsset(fileText);
        var csvStr = Resources.Load<TextAsset>(filePath);
        using (TextReader reader = new StringReader(csvStr.text))
        {
            var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var records = csv.GetRecords<Data>();

            dic.Clear();
            foreach (var record in records)
            {
                dic.Add(record.Mon_ID, record);
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
