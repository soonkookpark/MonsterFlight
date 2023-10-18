using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SaveDataVC = SaveDataV1;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public static class SaveLoadSystem
{
    public static int SaveDataVersion { get; } = 1;
    public static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}/Save";

        }
    }
    //�ϴ� ���̽� ����
    //����Ƽ���� singleton ȭ�� ������Ʈ�� �ϳ����� �� �����ض�
    public static void Save(SaveData data, string fileName)
    {
        //���丮�� �����ϴ°�?
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, fileName);

        //��������¿� �� ����ó���Ұ�
        using (var writer = new JsonTextWriter(new StreamWriter(path)))//�̰� �����ϴ°� ��Ʈ�� ������
        {
            var serializer = new JsonSerializer();
            //serializer.Converters.Add(new Vector3Converter());
            //serializer.Converters.Add(new QuaternionConverter());
            //�÷����̾ add ����
            //serializer.Converters.Add(new Vector3Converter());
            serializer.Serialize(writer, data);
        }//������ ������ �κ�
    }

    public static SaveData Load(string fileName)
    {
        var path = Path.Combine(SaveDirectory, fileName);
        if (!File.Exists(path))
        {
            return null;
        }

        SaveData data = null;
        int version = 0;

        var json = File.ReadAllText(path);
        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            var jObj = JObject.Load(reader);
            version = jObj["Version"].Value<int>();
        }
        using (var reader = new JsonTextReader(new StringReader(json)))
        {

            var serialize = new JsonSerializer();
            //serialize.Converters.Add(new Vector3Converter());
            //serialize.Converters.Add(new QuaternionConverter());
            switch (version)
            {
                case 1:
                    data = serialize.Deserialize<SaveDataV1>(reader);
                    break;
                

            }
            while (data.Version < SaveDataVersion)
            {
                //var oldData = data;
                //data = oldData.VersionUp();
                //version++;
                data = data.VersionUp();
            }
            //var serializer = new JsonSerializer();
            //data = serializer.Deserialize<SaveDataVC>(reader);
        }

        return data;
    }


    /*using (var file = File.OpenText(path))
    //{
    //    var serializer = new JsonSerializer();
    //    //�÷����̾ add ����
    //    //serializer.Converters.Add(new Vector3Converter());
    //    data = serializer.Deserialize(file, typeof(SaveDataV1)) as SaveData;
    //}*/

}

