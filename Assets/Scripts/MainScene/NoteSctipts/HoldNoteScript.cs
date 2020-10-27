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
            if (touchFlg)
            {
                holdGauge.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one / 2, Mathf.Abs(time / holdTime));
            }

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
