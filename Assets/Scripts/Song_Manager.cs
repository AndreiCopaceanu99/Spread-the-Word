using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.Audio;

public class Song_Manager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] Song;

    [SerializeField]
    AudioSource Source;

    public static Song_Manager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Source = GetComponent<AudioSource>();
        int r = Random.Range(0, 12);
        Source.clip = Song[r];
        Source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Source.isPlaying)
        {
            int r = Random.Range(0, 12);
            Source.clip = Song[r];
            Source.Play();
        }
    }
}
