using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;

public class sceneSwitcher : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    private bool b1;
    private bool b2;

    [SerializeField]
    [Tooltip("Name of the scene to be loaded when the button is selected.")]
    private string SceneToBeLoaded = "";

    [SerializeField]
    [Tooltip("Optional AudioClip which is played when the button is selected.")]
    private AudioClip audio_OnSelect = null;

    [SerializeField]
    [Tooltip("Timeout in seconds before new scene is loaded.")]
    private float waitTimeInSecBeforeLoading = 0.25f;
    public static string lastSceneLoaded = "";

    private void Start()
    {
        //button2.GetComponent<Interactable>().enabled = false;
        //button3.GetComponent<Interactable>().enabled = false;
        //button4.GetComponent<Interactable>().enabled = false;
    }

    private void Update()
    {
        //if(b1) button2.GetComponent<Interactable>().enabled = true;

        //if (b2) 
        //{ 
        //    button3.GetComponent<Interactable>().enabled = true; button4.GetComponent<Interactable>().enabled = true; }
    }
    public void Scene1()
    {
        SceneManager.LoadScene(1);
        b1 = true;
    }
    public void Scene2()
    {
        SceneManager.LoadScene(2);
        b2 = true;
    }
    public void Scene3()
    {
        SceneManager.LoadScene(3);
    }
    public void Scene4()
    {
        SceneManager.LoadScene(4);
    }

    public void AutoScene()
    {
        SceneManager.LoadScene(5);
    }

    public void ManualScene()
    {
        SceneManager.LoadScene(6);
    }

    //this is test


    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrWhiteSpace(sceneName))
        {
            StartCoroutine(LoadNewScene(sceneName));
        }
        else
        {
            Debug.Log($"Unsupported scene name: {sceneName}");
        }
    }

    private IEnumerator LoadNewScene(string sceneName)
    {
        //AudioFeedbackPlayer.Instance.PlaySound(audio_OnSelect);

        // Let's find out the name of the currently loaded additive scene to unload
        if (SceneManager.sceneCount > 1)
        {
            lastSceneLoaded = SceneManager.GetSceneAt(1).name;
            //lastSceneLoaded = "EyeTracker";
            Debug.Log($"Last scene name: {lastSceneLoaded}");

            // Let's wait in case we don't want to switch scenes too abruptly 
            yield return new WaitForSeconds(waitTimeInSecBeforeLoading);

            SceneManager.UnloadSceneAsync(sceneName);
        }

        Debug.Log($"New scene name: {SceneToBeLoaded}");
        lastSceneLoaded = SceneToBeLoaded;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
