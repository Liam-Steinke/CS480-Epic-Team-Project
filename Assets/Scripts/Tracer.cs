using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer : MonoBehaviour
{
    public Animator animator;
    public LineRenderer lineRenderer;

    void Start() {
        Destroy(gameObject, 0.2f);
        
    }

    public void AimAt(Vector3 start, Vector3 end) {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
