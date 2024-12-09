using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;

    //public GameObject levels

    [Header("Main Menu Buttons")]
    public Button startButton;

    //public Button LevelsSelectionButton;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;

    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        //SceneLoader.Start();
        //print("GSM starting");
        //Debug.Log("GSM startin g log");
        EnableMainMenu();
        //maybe better way, but this works well
        startButton.onClick.AddListener(StartGame);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);
        //LevelSelectionButton.onClick.AddListener(EnableLevelSelect);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        HideAll();
        //print("starting game");
        //print("SC.sing = " + SceneLoader.singleton);
        //SceneLoader.singleton.GoToSceneAsync("Level1");
        //print("gave to SC");
        SceneLoader.singleton.GoToSceneAsync(1);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        //AudioManager.singleton.UpdateBar();
        about.SetActive(false);
    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
    }
    // public void EnableLevelSelect()
    // {
    //     mainMenu.SetActive(false);
    //     options.SetActive(false);
    //     about.SetActive(false);


    // }
}
