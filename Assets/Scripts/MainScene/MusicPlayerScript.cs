using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    private AudioClip music;
    private AudioSource audioSource;

    public GameObject gameManager;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    public void MusicSet(string musicName)
    {
        music = Resources.Load("Musics/" + musicName) as AudioClip;
        audioSource.clip = music;
    }
}
