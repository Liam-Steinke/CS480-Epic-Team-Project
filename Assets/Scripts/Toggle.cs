using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Toggle : MonoBehaviour
{
    public InputActionReference toggleActionRef;
    public ActionBasedControllerManager rightLocomotion;
    private InputAction toggleAction;
    private Boolean SmoothMotion;

    // Start is called before the first frame update
    void Start()
    {
        toggleAction = toggleActionRef.ToInputAction();
        SmoothMotion = true;
        rightLocomotion.smoothMotionEnabled = false;
        //Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleAction.triggered)
        {
            //Debug.Log("A button Pressed");
            rightLocomotion.smoothMotionEnabled = SmoothMotion;
            SmoothMotion = !SmoothMotion;
        }
    }
}
