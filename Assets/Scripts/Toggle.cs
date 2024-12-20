using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Toggle : MonoBehaviour
{
    public InputActionReference toggleActionRef;

    public ActionBasedControllerManager leftLocomotion;

    public ActionBasedControllerManager rightLocomotion;

    public TextMeshProUGUI text;
    private InputAction toggleAction;
    private Boolean SmoothMotion;

    // Start is called before the first frame update
    void Start()
    {
        toggleAction = toggleActionRef.ToInputAction();
        SmoothMotion = true;
        rightLocomotion.smoothMotionEnabled = false;
        leftLocomotion.smoothMotionEnabled = true;
        updateText();
        //Debug.Log("Started");
    }

    void updateText()
    {
        if (!SmoothMotion)
        {
            text.SetText("Movement Mode: Free Motion");
        }
        else
        {
            text.SetText("Movement Mode: Teleportation");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //do nothing if paused 
        if (PauseMenu.paused)
        {
            return;
        }
        if (toggleAction.triggered)
        {
            //Debug.Log("A button Pressed");
            rightLocomotion.smoothMotionEnabled = SmoothMotion;
            leftLocomotion.smoothMotionEnabled = !SmoothMotion;
            SmoothMotion = !SmoothMotion;
            //text.SetText("changed");
            updateText();
        }
    }
}
