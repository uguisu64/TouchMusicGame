using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectSeceneScript : MonoBehaviour
{
    private GameObject dataStorage;
    private DataStorageScript dss;

    public Text musicName;
    public Text difficultyText;


    void Start()
    {
        dataStorage = GameObject.Find("DataStorage");
        dss = dataStorage.GetComponent<DataStorageScript>();
        MusicChange(dss.selectNumber);
    }

    public void PlusButton()
    {
        dss.selectNumber++;
        if (dss.selectNumber == dss.selectData.Count)
        {
            dss.selectNumber = 0;
        }
        MusicChange(dss.selectNumber);
    }
    public void MinusButton()
    {
        dss.selectNumber--;
        if (dss.selectNumber < 0)
        {
            dss.selectNumber = dss.selectData.Count - 1;
        }
        MusicChange(dss.selectNumber);
    }

    public void MusicChange(int musicNum)
    {
        musicName.text = dss.selectData[musicNum][0];
        difficultyText.text = dss.selectData[musicNum][1] + "  Lv." + dss.selectData[musicNum][2];
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
