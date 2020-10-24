using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalNoteScript : MonoBehaviour
{
    public float time;
    public GameObject moveNote;
    public GameObject judgeCircle;
    public GameObject gameManager;

    private float speed;

    private SpriteRenderer spriteRendererMN;
    private SpriteRenderer spriteRendererJC;


    void Start()
    {
        speed = 9 / time;
        spriteRendererMN = moveNote.GetComponent<SpriteRenderer>();
        spriteRendererJC = judgeCircle.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        time -= Time.deltaTime;
        Vector2 notePos = new Vector2(moveNote.transform.localPosition.x, moveNote.transform.localPosition.y - speed * Time.deltaTime);
        moveNote.transform.localPosition = notePos;

        if (time <= 0)
        {
            moveNote.transform.localPosition = Vector3.zero;
            spriteRendererMN.color -= new Color(0, 0, 0, Time.deltaTime * 5);
            spriteRendererJC.color -= new Color(0, 0, 0, Time.deltaTime * 5);

            //ミスノーツの消去
            if (time <= -0.2)
            {
                gameManager.GetComponent<MainSceneScript>().NoteMiss();
                Destroy(gameObject);
            }
        }
        
    }
}
