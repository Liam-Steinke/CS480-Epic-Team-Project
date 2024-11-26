using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerDoor : MonoBehaviour
{
    public UnityEvent triggered;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Throwable")) {
            if (triggered != null) {
                triggered.Invoke();
            }
        }
    }
}