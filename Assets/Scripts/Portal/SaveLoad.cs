using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad
{
    

    public static void Save(FakePlayerManager gm)
    {
        string filePath = Application.persistentDataPath + "/PlayerSave.pps";
        FileStream stream = new FileStream(filePath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        FakePlayerManager.FakeManagerToken data = new FakePlayerManager.FakeManagerToken(gm);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static FakePlayerManager.FakeManagerToken Load()
    {
        string filePath = Application.persistentDataPath + "/PlayerSave.pps";
        if (File.Exists(filePath))
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            FakePlayerManager.FakeManagerToken data = (FakePlayerManager.FakeManagerToken)bf.Deserialize(stream);
            stream.Close();
            return data;

        }

        return null;

    }

  
}
