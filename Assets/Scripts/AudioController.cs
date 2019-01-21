using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Timer))]
public class AudioController : MonoBehaviour {

    [SerializeField] AudioClip[] clips;
    [SerializeField] float delayBetweenClips;
    Timer Timer;

    bool canPlay;
    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        Timer = GetComponent<Timer>();
        canPlay = true;

	}
	
	// Update is called once per frame
	public void play () {

        if (!canPlay)
            return;

        Timer.Add(() =>
        {
            canPlay = true;
        }, delayBetweenClips);

        canPlay = false;

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        source.PlayOneShot(clip);
	}
}
