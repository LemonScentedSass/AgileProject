using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    /*
    public Image FadingImage;
    public GameObject FadingImageObject;
    public float fadeTime = 1;
    public float alpha;
    */

    public Button loadGenerationButton;
    public Button loadFeaturesButton;
    public Button quitButton;

    /*
    private void Awake()
    {
        FadingImageObject = GameObject.Find("Start Fade Img");
        FadingImage = FadingImageObject.GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(FadeImage());
    }

    private IEnumerator FadeImage()
    {
        for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
        {
            alpha = (i - 0) / (fadeTime - 0);
            FadingImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    private void Update()
    {
        if (alpha <= .1f)
        {
            FadingImage.gameObject.SetActive(false);
        }
    }
    */

    public void LoadGenerationScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadFeaturesScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
