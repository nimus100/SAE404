using UnityEngine;

public class DataProcessing : MonoBehaviour {
    public static DataProcessing Instance;

    void Start() {
        if (Instance == null)
            Instance = this;
    }

    public void fillSocket(int socketNo) {
        Debug.Log("socket filled: " + socketNo);
    }
}
