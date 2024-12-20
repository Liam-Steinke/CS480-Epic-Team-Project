using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    public AnimationCurve fadeCurve;
    public string colorPropertyName = "_Color";
    private Renderer rend;

    //private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        //mat = rend.material;
        //print("mat name = " + mat.name);
        //print(mat.GetColor(colorPropertyName));


        if (fadeOnStart)
        {
            // float old = fadeDuration;
            // fadeDuration = .01f;
            FadeIn();
            //fadeDuration = old;
        }
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Update()
    {

    }



    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        //make it visible
        rend.enabled = true;

        float timer = 0;
        //
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;

            newColor.a = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / fadeDuration)); //took from google

            rend.material.SetColor(colorPropertyName, newColor);
            //mat.SetColor(colorPropertyName, newColor);
            //print("should be a = " + newColor.a);
            //print("is be a = " + rend.material.color.a);

            //print("timer = " + timer);
            timer += Time.deltaTime; //wait for a frame to pass, might be better to put in update but this works
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor(colorPropertyName, newColor2);

        if (alphaOut == 0)
            rend.enabled = false;
    }
}
