using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FeatureUsage
{
    Once, //use once
    Toggle //if we want to use the features more than once
}

public class CoreFeatures : MonoBehaviour
{
    /*
     * some notes on what's going on in here (and probably some memes too)
     * - need a common way to access this code outside of this class 
     *   - create a property value, "peak laziness" (getters and setters)
     *     - this is ENCAPSULATION holy FUCK the connection has been made
     *       - he's calling setters accessors which I don't think is correct (mutators)
     * - enum: uses human-readable text to describe integers or something? I missed part of it
     *   - anything you want to make a list of and readable (think dropdown menus)
     * 
     * */

    public bool AudioSFXSourceCreated {  get; set; }
    
    [field: SerializeField]
    public AudioClip AudioClipOnStart { get; set; }

    [field: SerializeField]
    public AudioClip AudioClipOnEnd { get; set; }

    private AudioSource audioSource;

    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    {
        MakeAudioSFXSource();

    }

    private void MakeAudioSFXSource()
    {
        audioSource = GetComponent<AudioSource>();
        //if this is equal to null, create it right here
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        //regardless of null, we still need to make sure this is true on Awake
        AudioSFXSourceCreated = true;

    }

    //Audio play commands
    protected void PlayOnStart()
    {
        if(AudioSFXSourceCreated && AudioClipOnStart != null)
        {
            audioSource.clip = AudioClipOnStart;
            audioSource.Play();
        }

        
    }

    protected void PlayOnEnd()
    {
        if (AudioSFXSourceCreated && AudioClipOnEnd != null)
        {
            audioSource.clip = AudioClipOnEnd;
            audioSource.Play();
        }


    }
}
