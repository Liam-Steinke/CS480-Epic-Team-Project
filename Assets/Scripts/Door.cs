using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public UnityEvent door_event;

    void OnTriggerEnter(Collider other)
    {
        if (door_event != null)
        {
            door_event.Invoke();
        }
    }
}
