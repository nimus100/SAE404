using UnityEngine;

public enum SocketColor {
    red = 0,
    green = 1,
    blue = 2
}

public class SocketItem : MonoBehaviour {
    [Header("Parameters")] 
    public SocketColor color;
    public float forceThreshold = 2f;
    [Header("References")] 
    private AudioSource source;
    public AudioClip[] hitClips;

    void Start() {
        source = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        float collisionForce = collision.impulse.magnitude; // Get the force of impact

        if (collisionForce >= forceThreshold && hitClips.Length > 0)
        {
            PlayRandomSound(collisionForce);
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
}
