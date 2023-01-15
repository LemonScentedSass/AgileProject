using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace StatsSave
{
    public class StatsSaveLoad
    {
        public static void Save(FakePlayerManager gm)
        {
            string filePath = Application.persistentDataPath + "/PlayerStats.pps";
            FileStream stream = new FileStream(filePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            StatsSave.StatsToken.FakeManagerToken data = new StatsSave.StatsToken.FakeManagerToken(gm);
            bf.Serialize(stream, data);
            stream.Close();
        }

        public static StatsSave.StatsToken.FakeManagerToken Load()
        {
            string filePath = Application.persistentDataPath + "/Playerstats.pps";
            if (File.Exists(filePath))
            {
                FileStream stream = new FileStream(filePath, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                StatsSave.StatsToken.FakeManagerToken data = (StatsSave.StatsToken.FakeManagerToken)bf.Deserialize(stream);
                stream.Close();
                return data;

            }

            return null;

        }


    }
}

