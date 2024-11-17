using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public InputActionReference PauseActionRef;

    private InputAction PauseAction;

    private float oldTime;

    private Boolean paused;
    // Start is called before the first frame update
    void Start()
    {
        PauseAction = PauseActionRef.ToInputAction();
        oldTime = Time.timeScale;
        paused = false;
    }

    public void Pause()
    {
        print("Pausing");
        paused = true;
        Time.timeScale = 0.0f;
    }

    public void UnPause()
    {
        print("Unpausing");
        paused = false;
        Time.timeScale = oldTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseAction.triggered)
        {
            if (paused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }
}
