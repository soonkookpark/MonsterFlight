using System.Collections.Generic;
using UnityEngine;

public abstract class SaveData
{
    public int Version { get; set; }

    public abstract SaveData VersionUp();

}

public class SaveDataV1 : SaveData
{
    public SaveDataV1()
    {
        Version = 1;
    }


    //사용자에 대한 정보
    public int HighScore { get; set; }

    public override SaveData VersionUp()
    {
        return null;
    }

}

public class PlayerData
{
    public int highScore { get; set; }
    //public string Name { get; set; }
}