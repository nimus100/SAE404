using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ForceExplosion : MonoBehaviour {
    [Header("Parameters")] 
    public float force;
    public Transform origin;
    public float radius;
    [Header("References")]
    public List<Rigidbody> list = new List<Rigidbody>();

    void Start() {
        Explode();
    }   

    [Button("Explode")]
    void Explode() {
        foreach(var rb in list)
        {
            rb.AddExplosionForce(force, origin.position, radius);
        }
    }
}