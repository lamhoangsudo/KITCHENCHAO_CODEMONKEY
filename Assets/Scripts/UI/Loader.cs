using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MenuScenes,
        LoadingScenes,
        SampleScene
    }
    public static Scene targetScene;
    public static void Load(Scene scene)
    {
        targetScene = scene;
        SceneManager.LoadScene(Loader.Scene.LoadingScenes.ToString());
    }
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
