using SherlockGames.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(AudioSource))] //this will add audio sources with audio clips?
public class AudioManager : Singleton<AudioManager>
{//look to see if an array of audio clips are assigned, pick randomly through it and play them
    //this seems like a good backup plan in case my current idea doesn't work in time
    //what I want is to specify which audio plays at specific time
    //.wav is good for music, .ogg is good for SFX (quick, repetitive, and optimal sounds)
    //mp3 is godawful apparently
    //"these scripts do nothing at this point, which means they're bug-free. It's a wonder why anyone progresses past this point." - John

    //making things wait
    //can make things wait dependent on placement in array


    [Header("Background Music")]
    [SerializeField]
    private AudioClip[] tracks; //array of clips to randomize
    
    [SerializeField]
    private AudioSource audioSource; //plays the audio

    [Header("Events")]
    public Action onCurrentTrackEnd;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShuffleWhenStopPlaying());
        ShuffleAndPlay();
    }

    public void ShuffleAndPlay(GameState gameState = GameState.Playing) //ref enum class
    {
        if(tracks.Length > 0)
        {
            audioSource.clip = tracks[UnityEngine.Random.Range(0,tracks.Length - 1)];
            audioSource.Play();
        }
    }

    private IEnumerator ShuffleWhenStopPlaying() //another track starts playing when current one ends
    {
        while(true) //flashback to josh messing with the disc drive to open and close infinitely
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            ShuffleAndPlay();
            onCurrentTrackEnd?.Invoke(); //invokes this action to anything that is listening
        }
    }
}
