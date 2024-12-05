using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private BoxCollider collider;
    public bool startOpen = true;
    public UnityEvent doorOpened;
    public UnityEvent doorClosed;

    void Start() {
        collider = gameObject.GetComponent<BoxCollider>();
        if (startOpen) {
            openDoor();
        }
    }
    
    public void openDoor() {
        collider.enabled = false;
        doorOpened.Invoke();
    }

    public void closeDoor() {
        collider.enabled = true;
        doorClosed.Invoke();
    }

    

    // void OnTriggerEnter(Collider other)
    // {
    //     if (door_event != null)
    //     {
    //         door_event.Invoke();
    //     }
    // }
}
