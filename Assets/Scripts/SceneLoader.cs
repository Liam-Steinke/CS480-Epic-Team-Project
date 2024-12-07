using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //'game object' that will fade in and out
    public FadeScreen fadeScreen;
    //keep a reference of the script so that other scripts can access it
    public static SceneLoader singleton; //thanks bernstein!
    //Awake gets called before all other methods, was getting weird error when it was in start

    private void Awake()
    {
        //print("SC Awake");
        //Debug.Log("SC Awake");
        //check if singleton is started, if so and is different this get rid of the other one
        if (singleton && singleton != this)
            Destroy(singleton);

        singleton = this;
    }

    public void Start()
    {
        //print("SC starting");
        //Debug.Log("SC starting ");
        //set singletion to this instance
        singleton = this;
        //print("Start done SC");

    }

    public Boolean mainMenu()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name.Equals("StartMenu");
    }

    public void GoToScene(int sceneIndex)
    {
        //wrapper method to make calling easier and start everything from this script
        //print("starting scne e = " + sceneIndex);
        StartCoroutine(GoToSceneRoutine(sceneIndex));

    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        //fade out
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        //Launch the new scene
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToSceneAsync(int sceneIndex)
    {
        //lets fade run while scene is being loaded
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        //Launch the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); //load the scene then let fade finish
        operation.allowSceneActivation = false;

        float timer = 0;
        //wait for the scene to finish loading and fade animation to be done
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //do switch 
        operation.allowSceneActivation = true;
    }

    IEnumerator GoToSceneAsyncRoutine(String sceneName)
    {
        fadeScreen.FadeOut();
        //Launch the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName); //load the scene then let fade finish
        operation.allowSceneActivation = false;

        float timer = 0;
        //wait for the scene to finish loading and fade animation to be done
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //do switch 
        operation.allowSceneActivation = true;
    }


    //Method for fading to a new scene using the scene name instead of the scene index
    public void GoToSceneAsync(String sceneName)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneName));

        // print("SceneManager.loadedSceneCount = " + SceneManager.loadedSceneCount);
        // print("SceneManager.sceneCount = " + SceneManager.sceneCount);
        // print("SceneManager.sceneCountInBuildSettings = " + SceneManager.sceneCountInBuildSettings);
        // Scene scene = SceneManager.GetSceneByName(sceneName);
        // if (!scene.IsValid())
        // {
        //     int i = 0;
        //     while (i < SceneManager.sceneCountInBuildSettings)
        //     {
        //         scene = SceneManager.GetSceneAt(i);
        //         print("scene name(" + i + ")" + " = " + scene.name);
        //         if (scene.name.Equals(sceneName))
        //         {
        //             break;
        //         }
        //         i++;
        //     }

        // }
        // StartCoroutine(GoToSceneAsyncRoutine(scene.buildIndex));

    }


    //method for resetting/restarting the current scene
    public void resetScene()
    {
        int a = SceneManager.GetActiveScene().buildIndex;
        GoToSceneAsync(a);
    }
}
