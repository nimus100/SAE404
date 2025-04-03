using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractionItem : MonoBehaviour
{
    public AudioClip[] hitClips;
    public float forceThreshold = 2f;

    public bool playFollowUp = false;
    public float timeDelay = 1f;
    public AudioClip[] followUpClips;
    
    private AudioSource source;

    private void Start() {
        source = GetComponent<AudioSource>();
        GravityManager.GetInstance().addToGravityDisablable(GetComponent<Rigidbody>());
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        float collisionForce = collision.impulse.magnitude; // Get the force of impact

        if (collisionForce >= forceThreshold && hitClips.Length > 0)
        {
            PlayRandomSound(collisionForce);
            if (playFollowUp)
                StartCoroutine(WaitAndPlay(timeDelay));
        }
    }

    private void PlayRandomSound(float volume)
    {
        if (source != null && hitClips.Length > 0)
        {
            AudioClip randomClip = hitClips[Random.Range(0, hitClips.Length)];
            source.pitch = Random.Range(0.8f, 1.2f);
            volume = (volume > 1f) ? 1f : volume;
            source.PlayOneShot(randomClip, volume);
        }
    }

    private IEnumerator WaitAndPlay(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        
        source.PlayOneShot(followUpClips[Random.Range(0, followUpClips.Length)]);
    }
}
