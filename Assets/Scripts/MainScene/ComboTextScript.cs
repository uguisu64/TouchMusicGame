using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTextScript : MonoBehaviour
{
    private Text comboText;

    private float time;
    void Start()
    {
        comboText = gameObject.GetComponent<Text>();
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.4)
        {
            comboText.text = "";
        }
    }

    public void MoveComboText(int combo,Vector3 position)
    {
        comboText.text = combo.ToString();
        comboText.rectTransform.position = Camera.main.WorldToScreenPoint(position);
        time = 0;
    }
}
