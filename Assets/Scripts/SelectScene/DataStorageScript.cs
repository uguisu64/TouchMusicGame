using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorageScript : MonoBehaviour
{
    public static GameObject instance;

    public List<string[]> selectData;
    public int selectNumber;
    private CSVReader csvReader;

    public int perfect;
    public int great;
    public int good;
    public int bad;
    public int miss;
    public int combo;
    public float score;

    void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        selectNumber = 0;
        csvReader = gameObject.GetComponent<CSVReader>();
        selectData = csvReader.CsvRead("CsvFiles/others/SelectData");
    }
}
