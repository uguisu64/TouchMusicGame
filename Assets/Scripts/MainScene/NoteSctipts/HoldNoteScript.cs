using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNoteScript : MonoBehaviour
{

    public float time;
    public GameObject moveNote;
    public GameObject judgeCircle;
    public GameObject gameManager;
    public GameObject holdGauge;

    private float speed;

    private SpriteRenderer spriteRendererMN;
    private SpriteRenderer spriteRendererJC;

    
    private bool touchFlg;

    //Holdの長さ
    public float holdTime;

    private float touchTime;

    void Start()
    {
        speed = 9 / time;
        spriteRendererMN = moveNote.GetComponent<SpriteRenderer>();
        spriteRendererJC = judgeCircle.GetComponent<SpriteRenderer>();

        //HoldNote
        touchFlg = false;
    }


    void Update()
    {
        time -= Time.deltaTime;
        Vector2 notePos = new Vector2(moveNote.transform.localPosition.x, moveNote.transform.localPosition.y - speed * Time.deltaTime);
        moveNote.transform.localPosition = notePos;

        if (time <= 0)
        {
            //ノーツの消去
            if (time <= -0.2)
            {
                if (!touchFlg)
                {
                    gameManager.GetComponent<MainSceneScript>().NoteMiss();
                    Destroy(gameObject);
                }
                else if (time <= -holdTime)
                {
                    gameManager.GetComponent<MainSceneScript>().AddScore(Mathf.Abs(touchTime), transform.position);
                    Destroy(gameObject);
                }
                touchFlg = false;
            }

            if (touchFlg)
            {
                holdGauge.transform.localScale += new Vector3(0.5f / holdTime * Time.deltaTime, 0.5f / holdTime * Time.deltaTime, 1);
            }

            moveNote.transform.localPosition = Vector3.zero;
            spriteRendererMN.color -= new Color(0, 0, 0, Time.deltaTime * 5);
            spriteRendererJC.color -= new Color(0, 0, 0, Time.deltaTime * 5);
        }
    }

    public void NoteTouchBegan()
    {
        if (time <= 0)
        {
            if (touchFlg)
            {
                touchTime = 0;
            }
            else
            {
                touchTime = time;
            }
        }
    }

    public void NoteTouchHold()
    {
        touchFlg = true;
    }
}
