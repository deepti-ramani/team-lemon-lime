using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//grants access to Audio Mixer
using UnityEngine.Audio;

public class RandomContainer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioMixerGroup output;
    public float minPitch = 0.75f;
    public float maxPitch = 1.25f;

    public bool isPlaying = false;

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            isPlaying = true;
            PlaySound();
        }
        if(gameObject.GetComponent<AudioSource>() == null)
        {
            isPlaying = false;
        }
    }

    public void PlaySound()
    {
        //randomize within array length 
        int randomClip = Random.Range(0, clips.Length);

        //create audiosource
        AudioSource source = gameObject.AddComponent<AudioSource>();

        //load clip to audiosource
        source.clip = clips[randomClip];

        //set output for the audiosource
        source.outputAudioMixerGroup = output;
        
        //set pitch variation
        source.pitch = Random.Range(minPitch, maxPitch);

        //play clip 
        source.Play();

        //derstroy audiosource when done, after full length of clip
        Destroy(source, clips[randomClip].length);
    }
}
