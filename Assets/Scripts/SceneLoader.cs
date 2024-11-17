using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


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

        singleton = this;
        //print("Start done SC");
    }

    public void GoToScene(int sceneIndex)
    {
        //print("starting scne e = " + sceneIndex);
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        //Launch the new scene
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        //Launch the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }


}
