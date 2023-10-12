using System;
using System.Collections.Generic;
using UnityEngine;


public static class DataTableMgr
{
    private static Dictionary<Type, DataTable> tables = new Dictionary<Type, DataTable>();

    static DataTableMgr()
    {
        tables.Clear();
        var MonsterTable = new MonsterTable();
        tables.Add(typeof(MonsterTable), MonsterTable);
    }

    public static T GetTable<T>() where T : DataTable
    {
        var id = typeof(T);
        if (!tables.ContainsKey(id))
        {
            return null;
        }
        return tables[id] as T;
    }

    public static void LoadAll()
    {
        //tables.Add(, new MyDataTable());
        Debug.Log(tables);
        foreach (var item in tables)
        {
            item.Value.Load();
        }
    }
}
