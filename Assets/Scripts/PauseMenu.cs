using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using TMPro;
//using UnityEditor.ProBuilder;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;


public class PauseMenu : MonoBehaviour
{
    public InputActionReference PauseActionRef;

    private InputAction PauseAction;


    public static Boolean paused;
    public GameObject LocomotionSystem;
    public GameObject menuRoot;
    public GameObject player;

    public GameObject Camera;

    [Header("UI Pages")]

    public GameObject pauseMenu;
    public GameObject options;
    //public GameObject about;

    //public GameObject levels

    [Header("Pause Menu Buttons")]
    public Button resumeButton;

    //public Button LevelsSelectionButton;
    public Button optionButton;
    public Button resetButton;
    public Button mainMenuButton;

    public List<Button> returnButtons;




    void Awake()
    {
        PauseAction = PauseActionRef.ToInputAction();
        paused = false;
        LocomotionSystem.SetActive(true);
        verifyPlayer();
    }

    private void verifyPlayer()
    {
        if (player == null)
        {
            player = GameObject.Find("XR Origin(XR Rig)");
        }
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseAction = PauseActionRef.ToInputAction();
        paused = false;
        menuRoot.SetActive(false);
        verifyPlayer();
        LocomotionSystem.SetActive(true);
    }

    void mainMenu()
    {
        HideAll();
        SceneLoader.singleton.GoToSceneAsync("StartMenu");
    }

    public void enablePauseMenu()
    {
        pauseMenu.SetActive(true);
        options.SetActive(false);
    }

    public void Reset()
    {
        HideAll();
        SceneLoader.singleton.resetScene();

    }

    public void EnableOption()
    {
        pauseMenu.SetActive(false);
        options.SetActive(true);
        //AudioManager.singleton.UpdateBar();
    }

    public void Pause()
    {

        //print("Pausing");
        paused = true;
        FixPosition();
        LocomotionSystem.SetActive(false);
        menuRoot.SetActive(true);
        pauseMenu.SetActive(true);
        resumeButton.onClick.AddListener(UnPause);
        optionButton.onClick.AddListener(EnableOption);
        resetButton.onClick.AddListener(Reset);
        mainMenuButton.onClick.AddListener(mainMenu);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(enablePauseMenu);
        }


    }

    public void FixPosition()
    {
        float scale = 3.0f;
        verifyPlayer();
        Vector3 pp = player.transform.position;
        pp.y += 1.3f;

        Transform ct = Camera.transform;
        float Rotation;
        if (ct.eulerAngles.y <= 180.0f)
        {
            Rotation = ct.eulerAngles.y;
        }
        else
        {
            Rotation = ct.eulerAngles.y - 360.0f;
        }
        //print("Rotation = " + Rotation);
        Vector3 eulerangle = Camera.transform.eulerAngles;
        eulerangle.x = 0.0f;
        eulerangle.z = 0.0f;
        eulerangle.y = Rotation;
        menuRoot.transform.eulerAngles = eulerangle;
        float radian = Mathf.Deg2Rad * Rotation;
        pp.x += Mathf.Sin(radian) * scale;
        pp.z += Mathf.Cos(radian) * scale;
        menuRoot.transform.position = pp;

    }

    public void HideAll()
    {
        pauseMenu.SetActive(false);
        options.SetActive(false);
        menuRoot.SetActive(false);
    }

    public void UnPause()
    {
        //print("Unpausing");
        paused = false;
        HideAll();
        LocomotionSystem.SetActive(true);
        //menuRoot.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (PauseAction.triggered)
        {
            if (SceneLoader.singleton.mainMenu())
            {
                return;
            }
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
