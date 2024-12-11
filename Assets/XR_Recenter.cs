using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XR_Recenter : MonoBehaviour
{
    public Transform xrOrigin;
    public Transform xrCamera;

    public bool recenterOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if (recenterOnStart)
        {
            Invoke("Recenter", 0.05f);
        }
    }

    public void Recenter()
    {
        Vector3 offset = xrCamera.position - xrOrigin.position;
        xrOrigin.position = new Vector3(transform.position.x - offset.x, transform.position.y, transform.position.z - offset.z);
    }
}