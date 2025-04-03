using UnityEngine;

public enum SocketColor {
    red = 0,
    green = 1,
    blue = 2
}

public class SocketItem : MonoBehaviour {
    [Header("Parameters")] 
    public SocketColor color;
}
