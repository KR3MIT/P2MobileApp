using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataToCSV : MonoBehaviour
{
    private string filepath = "";
    private string path = "";
    private int fileCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath;
        filepath = path + "/acceleration.csv";
    }

    public void SerializeCSV(float data, string name)
    {
        filepath = path + "/player data" + System.DateTime.Today.ToString("yyyyMMdd") + "-" + fileCounter + ".csv";

        while (File.Exists(filepath))
        {
            fileCounter++;
            filepath = path + "/player data" + System.DateTime.Today.ToString("yyyyMMdd") + "-" + fileCounter + ".csv";
        }

        StreamWriter sw = new StreamWriter(filepath);

        sw.WriteLine(name);

        sw.WriteLine(data);

        sw.Flush();
        sw.Close();
    }
}
