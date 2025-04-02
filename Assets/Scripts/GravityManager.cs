using System;
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
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public static GravityManager Instance;

    private bool enableGravity = true;

    private void Start() {
        if (Instance == null)
            Instance = this;
    }
    
    public void GravityEnable() {
        SetGravity(true);
        Debug.Log("Gravity enabled");
    }
    public void GravityDisable() {
        SetGravity(false);
        Debug.Log("Gravity disabled");
    }

    [Button("Toggle Gravity")]
    public void GravityToggle() {
        enableGravity = !enableGravity;
        SetGravity(enableGravity);
    }

    private void SetGravity(bool enable) {
        if (enable) {
            foreach(var rb in rigidbodies) {
                rb.useGravity = true;
            }
        }
        else {
            foreach (var rb in rigidbodies) {
                rb.useGravity = false;
                rb.AddForce(new Vector3(0, upForce.x + Random.Range(0, upForce.y), 0), ForceMode.Impulse);
                rb.AddTorque(RandomVector3(rotation.x, rotation.y));
            }
        }
    }

    private Vector3 RandomVector3(float min, float max) {
        return new Vector3(
            (Random.Range(0, 1) % 2 == 0) ? Random.Range(min, max) : 0,
            (Random.Range(0, 1) % 2 == 0) ? Random.Range(min, max) : 0,
            (Random.Range(0, 1) % 2 == 0) ? Random.Range(min, max) : 0);
    }
}
