using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataToCSV : MonoBehaviour
{
    private string filepath = "";
    private string path = "";
    private int fileCounter = 1;
    private Character player;

    // Start is called before the first frame update
    void Start()
    {
        player = Character.instance;
        path = Application.persistentDataPath;
    }

    public void SerializeCSV(float data, string name)
    {
        filepath = path + "/" + player.playerName + "data" + System.DateTime.Today.ToString("yyyyMMdd") + "-" + fileCounter + ".csv";

        while (File.Exists(filepath))
        {
            fileCounter++;
            filepath = path + "/" + player.playerName + "data" + System.DateTime.Today.ToString("yyyyMMdd") + "-" + fileCounter + ".csv";
        }

        StreamWriter sw = new StreamWriter(filepath);

        sw.WriteLine(name);

        sw.WriteLine(data);

        sw.Flush();
        sw.Close();
    }
}
