using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class GravityManager : MonoBehaviour {
    [Header("Parameters")] 
    [Tooltip("fist value - force \nsecond value - random deviation")]
    public Vector2 upForce = new Vector2(0.1f, 0.05f);
    public Vector2 rotation = new Vector2(-0.2f, 0.2f);
    [Header("References")] 
    private AudioSource source;
    private AudioClip onClip;
    private AudioClip offClip;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    private static GravityManager Instance;

    private bool enableGravity = true;

    private void Start() {
        if (Instance == null)
            Instance = this;
    }

    public static GravityManager GetInstance() {
        return Instance;
    }

    public void addToGravityDisablable(Rigidbody rb) {
        rigidbodies.Add(rb);
    }

    [Button("[debug] Toggle Gravity [debug]")]
    public void GravityToggle() {
        enableGravity = !enableGravity;
        SetGravity(enableGravity);
    }

    public void ScheduleGravityDisable(float timeToDisable) {
        StartCoroutine(WaitAndDisableGrabity(timeToDisable));
    }

    private void SetGravity(bool enable) {
        if (enable) {
            foreach(var rb in rigidbodies) {
                rb.useGravity = true;
                //source.PlayOneShot(onClip);
            }
        }
        else {
            foreach (var rb in rigidbodies) {
                rb.useGravity = false;
                rb.AddForce(new Vector3(0, upForce.x + Random.Range(0, upForce.y), 0), ForceMode.Impulse);
                rb.AddTorque(RandomVector3(rotation.x, rotation.y));
               // source.PlayOneShot(offClip);
            }
        }
    }

    private Vector3 RandomVector3(float min, float max) {
        return new Vector3(
            (Random.Range(0, 1) % 2 == 0) ? Random.Range(min, max) : 0,
            (Random.Range(0, 1) % 2 == 0) ? Random.Range(min, max) : 0,
            (Random.Range(0, 1) % 2 == 0) ? Random.Range(min, max) : 0);
    }

    private IEnumerator WaitAndDisableGrabity(float timeToDisable) {
        // play warning sound
        yield return new WaitForSeconds(timeToDisable);
        
        // check if player has pressed the button
    }
}
