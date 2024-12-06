using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private BoxCollider doorCollider;
    public bool startOpen = true;
    public UnityEvent doorOpened;
    public UnityEvent doorClosed;

    void Start() {
        doorCollider = gameObject.GetComponent<BoxCollider>();
        if (startOpen) {
            openDoor();
        }
    }
    
    public void openDoor() {
        doorCollider.enabled = false;
        doorOpened.Invoke();
    }

    public void closeDoor() {
        doorCollider.enabled = true;
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
