using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public List<string[]> CsvRead(string musicName)
    {
        List<string[]> csvDatas = new List<string[]>();
        TextAsset csv = Resources.Load(musicName) as TextAsset;
        StringReader stringReader = new StringReader(csv.text);
        while (stringReader.Peek() > -1)
        {
            string line = stringReader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        return csvDatas;
    }
}
