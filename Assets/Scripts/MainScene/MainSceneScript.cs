using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneScript : MonoBehaviour
{
    private int perfect;
    private int great;
    private int good;
    private int bad;
    private int miss;
    private int combo;
    private float score;
    private float aScore;

    public Text scoreText;
    public Text comboText;
    private ComboTextScript cts;

    private GameObject dataStorage;
    private DataStorageScript dss;

    public GameObject musicPlayer;
    public GameObject notesGenerator;
    private NotesGenerator ns;

    public GameObject perfectParticle;
    public GameObject greatParticle;
    public GameObject goodParticle;
    public GameObject badParticle;


    void Start()
    {
        dataStorage = GameObject.Find("DataStorage");
        dss = dataStorage.GetComponent<DataStorageScript>();

        musicPlayer.GetComponent<MusicPlayerScript>().MusicSet(dss.selectData[dss.selectNumber][4]);
        ns = notesGenerator.GetComponent<NotesGenerator>();
        ns.MusicScoreRead(dss.selectData[dss.selectNumber][6], dss.selectData[dss.selectNumber][5]);
        ns.StartCoroutine(ns.BpmChange());
        ns.StartCoroutine(ns.MKnote());

        cts = comboText.GetComponent<ComboTextScript>();

        score = 0;
        aScore = 100.0f / ns.csvDatas.Count;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, 10);
                    
                    if (hit2Ds.Length != 0)
                    {
                        RaycastHit2D hit2D = hit2Ds[0];

                        for (int j = 1; j < hit2Ds.Length; j++)
                        {
                            if (hit2Ds[j].transform.gameObject.GetComponent<NomalNoteScript>().time < hit2D.transform.gameObject.GetComponent<NomalNoteScript>().time)
                            {
                                hit2D = hit2Ds[j];
                            }
                        }

                        if (hit2D.transform.tag == "NomalNote")
                        {
                            AddScore(hit2D.transform.gameObject.GetComponent<NomalNoteScript>().time, hit2D.transform.position);
                            Destroy(hit2D.transform.gameObject);
                        }
                    }
                    
                }
            }
        }
    }

    void AddScore(float timing, Vector3 position)
    {
        if (Mathf.Abs(timing) <= 0.06)
        {
            //Perfect
            perfect++;
            combo++;
            score += aScore;
            Instantiate(perfectParticle, position, Quaternion.identity);
        }
        else if (Mathf.Abs(timing) <= 0.1)
        {
            //Great
            great++;
            combo++;
            score += aScore * 0.9f;
            Instantiate(greatParticle, position, Quaternion.identity);
        }
        else if (Mathf.Abs(timing) <= 0.2)
        {
            //Good
            good++;
            combo++;
            score += aScore * 0.5f;
            Instantiate(goodParticle, position, Quaternion.identity);
        }
        else
        {
            //Bad
            bad++;
            combo = 0;
            score += aScore * 0.1f;
            Instantiate(badParticle, position, Quaternion.identity);
        }
        scoreText.text = score.ToString("F2") + "%";
        cts.MoveComboText(combo, position);
    }

    public void NoteMiss()
    {
        miss++;
        combo = 0;
    }

    public IEnumerator GameEnd(float timing)
    {
        yield return new WaitForSeconds(timing + 0.5f);

        dss.perfect = perfect;
        dss.great = great;
        dss.good = good;
        dss.bad = bad;
        dss.miss = miss;
        dss.combo = combo;
        dss.score = score;
        SceneManager.LoadScene("ResultScene");
    }
}
