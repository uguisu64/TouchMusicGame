using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    //ノーツprefab
    public GameObject nomalNote;
    public GameObject gameManager;
    public GameObject musicPlayer;

    private MainSceneScript mss;

    private float time;
    private float bpm;
    private int measure;
    private int bpmSerialNumber;

    private CSVReader csvReader;
    public List<string[]> csvDatas;
    private List<string[]> musicDatas;


    void Update()
    {
        time += Time.deltaTime;
    }

    void NomalNoteCreate(Vector3 posision, float rotate, float timing)
    {
        GameObject note = Instantiate(nomalNote, posision, Quaternion.identity);
        note.transform.Rotate(new Vector3(0, 0, rotate));
        NomalNoteScript nns = note.GetComponent<NomalNoteScript>();
        nns.time = timing;
        nns.gameManager = gameManager;
    }

    public IEnumerator BpmChange()
    {
        for(int i = 0; i < musicDatas.Count; i++)
        {
            bpm = float.Parse(musicDatas[i][2]);
            measure = int.Parse(musicDatas[i][3]);
            bpmSerialNumber = i;
            if (i + 1 < musicDatas.Count)
            {
                float nexttiming = ((int.Parse(musicDatas[i + 1][0]) - int.Parse(musicDatas[i][0])) * measure + int.Parse(musicDatas[i + 1][1]) - int.Parse(musicDatas[i][1])) / 2 / bpm;
                nexttiming += Beat(bpm) * 3;
                yield return new WaitForSeconds(nexttiming);
            }
        }
    }

    public IEnumerator MKnote()
    {
        float beat = Beat(bpm);
        float nexttiming = 0;

        StartCoroutine(DelayMusicPlay(Beat(bpm) * 4));       
        nexttiming += ((int.Parse(csvDatas[0][1]) - 1) * measure + int.Parse(csvDatas[0][2])) / 2 / bpm;
        nexttiming += Beat(bpm) * 3;

        mss = gameManager.GetComponent<MainSceneScript>();
        mss.StartCoroutine(mss.GameEnd(musicPlayer.GetComponent<AudioSource>().clip.length + Beat(bpm) * 4));

        yield return new WaitForSeconds(nexttiming);

        for (int i = 0; i < csvDatas.Count; i++)
        {
            Vector3 notePos = new Vector3(float.Parse(csvDatas[i][3]), float.Parse(csvDatas[i][4]), 0);
            NomalNoteCreate(notePos, float.Parse(csvDatas[i][5]), beat);
            while (i + 1 < csvDatas.Count && csvDatas[i][1] == csvDatas[i + 1][1] && csvDatas[i][2] == csvDatas[i + 1][2])
            {
                i++;
                notePos = new Vector3(float.Parse(csvDatas[i][3]), float.Parse(csvDatas[i][4]), 0);
                NomalNoteCreate(notePos, float.Parse(csvDatas[i][5]), beat);
            }
            if (i + 1 < csvDatas.Count)
            {
                int currentBar = int.Parse(csvDatas[i][1]);
                int currentTiming = int.Parse(csvDatas[i][2]);
                int cBpmSerialNumber = bpmSerialNumber + 1;
                int cMeasure = measure;
                float cBpm = bpm;
                nexttiming -= time;
                time = 0;
                if (cBpmSerialNumber < musicDatas.Count)
                {
                    while (int.Parse(csvDatas[i + 1][1]) >= int.Parse(musicDatas[cBpmSerialNumber][0]))
                    {
                        if (int.Parse(csvDatas[i + 1][1]) == int.Parse(musicDatas[cBpmSerialNumber][0]) && int.Parse(csvDatas[i + 1][2]) <= int.Parse(musicDatas[cBpmSerialNumber][0]))
                        {
                            break;
                        }
                        nexttiming += ((int.Parse(musicDatas[cBpmSerialNumber][0]) - currentBar) * cMeasure + int.Parse(musicDatas[cBpmSerialNumber][1]) - currentTiming) / 2 / cBpm;
                        currentBar = int.Parse(musicDatas[cBpmSerialNumber][0]);
                        currentTiming = int.Parse(musicDatas[cBpmSerialNumber][1]);
                        cMeasure = int.Parse(musicDatas[cBpmSerialNumber][3]);
                        cBpm = float.Parse(musicDatas[cBpmSerialNumber][2]);
                        cBpmSerialNumber++;
                        if (cBpmSerialNumber == musicDatas.Count)
                        {
                            break;
                        }
                    }
                }
                nexttiming += ((int.Parse(csvDatas[i + 1][1]) - currentBar) * cMeasure + int.Parse(csvDatas[i + 1][2]) - currentTiming) / 2 / cBpm;
                nexttiming += Beat(bpm) - Beat(cBpm);
                beat = Beat(cBpm);
                yield return new WaitForSeconds(nexttiming);
            }
        }
    }

    IEnumerator DelayMusicPlay(float timing)
    {
        yield return new WaitForSeconds(timing);

        musicPlayer.GetComponent<AudioSource>().Play();
    }

    float Beat(float bpm)
    {
        if (bpm < 120)
        {
            return 60 / bpm;
        }
        else if (bpm >= 120 && bpm < 240)
        {
            return 120 / bpm;
        }
        else
        {
            return 180 / bpm;
        }
    }

    public void MusicScoreRead(string scoreName,string musicDataName)
    {
        csvReader = gameObject.GetComponent<CSVReader>();
        csvDatas = csvReader.CsvRead("CsvFiles/MusicalScores/" + scoreName);
        musicDatas = csvReader.CsvRead("CsvFiles/MusicalScores/" + musicDataName);
        bpm = float.Parse(musicDatas[0][2]);
    }
}
