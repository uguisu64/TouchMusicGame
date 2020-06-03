using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneScript : MonoBehaviour
{
    private GameObject dataStorage;
    private DataStorageScript dss;

    public Text musicText;
    public Text difficultyText;
    public Text perfectText;
    public Text greatText;
    public Text goodText;
    public Text badText;
    public Text missText;
    public Text scoreText;

    
    void Start()
    {
        dataStorage = GameObject.Find("DataStorage");
        dss = dataStorage.GetComponent<DataStorageScript>();
        musicText.text = dss.selectData[dss.selectNumber][0];
        difficultyText.text = dss.selectData[dss.selectNumber][1] + "  Lv." + dss.selectData[dss.selectNumber][2];
        perfectText.text = dss.perfect.ToString();
        greatText.text = dss.great.ToString();
        goodText.text = dss.good.ToString();
        badText.text = dss.bad.ToString();
        missText.text = dss.miss.ToString();
        scoreText.text = dss.score.ToString("F2") + "%";
    }

    public void NextButton()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
