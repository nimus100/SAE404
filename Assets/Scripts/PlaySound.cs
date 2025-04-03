using System;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    public AudioClip clip;
    private AudioSource source;
    private void Start() {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudioClip() {
        source.PlayOneShot(clip);
    }
}
