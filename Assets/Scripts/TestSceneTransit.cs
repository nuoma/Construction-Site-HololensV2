using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Extensions.SceneTransitions;

public class TestSceneTransit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //scene loader
    //https://stackoverflow.com/questions/65075941/how-do-i-properly-reload-a-unity-scene-that-uses-mrtk-for-the-hololens2/65076801
    private void Awake()
    {
        // Register callback for everytime a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Initially load the second scene additive
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    // Called when a new scene was loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (loadMode == LoadSceneMode.Additive)
        {
            // Set the additive loaded scene as active
            // So new instantiated stuff goes here
            // And so we know which scene to unload later
            SceneManager.SetActiveScene(scene);
        }
    }


    public static void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        var index = currentScene.buildIndex;

        SceneManager.UnloadSceneAsync(currentScene);

        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }

    public static void LoadScene(string name)
    {
        var currentScene = SceneManager.GetActiveScene();

        SceneManager.UnloadSceneAsync(currentScene);

        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public static void LoadScene(int index)
    {
        var currentScene = SceneManager.GetActiveScene();

        SceneManager.UnloadSceneAsync(currentScene);

        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }


}
