using System.Collections;
using PrimeTween;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketValidator : MonoBehaviour {
    public SocketColor requiredColor;
    public AudioClip insertClip;
    public AudioClip exitClip;
    public AudioClip failureClip;
    
    private XRSocketInteractor socket;
    private AudioSource source;

    void Start() {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnItemInserted);
        socket.selectExited.AddListener(OnItemExited);
        
        source = GetComponent<AudioSource>();
    }
    
    private void OnDestroy() {
        socket.selectEntered.RemoveListener(OnItemInserted);
    }

    private void OnItemInserted(SelectEnterEventArgs args) {
        IXRSelectInteractable interactable = args.interactableObject;
        XRGrabInteractable grabbedItem = interactable.transform.GetComponent<XRGrabInteractable>();

        if (grabbedItem != null) {
            SocketItem item = grabbedItem.GetComponent<SocketItem>();

            if (item.color != requiredColor){
                StartCoroutine(EjectWithGrabDelay(interactable));
            }
            else {
                DataProcessing.Instance.fillSocket((int) requiredColor);
                Tween.Position(item.transform, 
                    item.transform.position, 
                    item.transform.position + new Vector3(-0.3f, 0, 0), 
                    0.7f);
                source.PlayOneShot(insertClip);
            }
        }
    }

    private void OnItemExited(SelectExitEventArgs args) {
        source.PlayOneShot(exitClip);
    }

    private IEnumerator EjectWithGrabDelay(IXRSelectInteractable item) {
        XRGrabInteractable grab = item.transform.GetComponent<XRGrabInteractable>();
        
        // Force eject by deselecting
        socket.interactionManager.SelectExit(socket, item);
        item.transform.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        source.PlayOneShot(failureClip);

        // Disable collisions for a short time to prevent instant re-grab
        if (grab != null) {
            grab.enabled = false;
        }

        yield return new WaitForSeconds(1.5f); // Adjust delay time as needed

        if (grab != null) {
            grab.enabled = true;
        }
    }
}
