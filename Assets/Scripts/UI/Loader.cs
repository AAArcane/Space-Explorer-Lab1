using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene loading in the game, allowing for transitions between different scenes.
/// </summary>
public static class Loader
{
    // Enum representing different scenes in the game
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }

    private static Scene targetScene; // The scene to be loaded

    /// <summary>
    /// Initiates the loading of a specified scene.
    /// </summary>
    /// <param name="targetScene">The scene to load.</param>
    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene; // Set the target scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString()); // Load the loading scene
    }

    /// <summary>
    /// Callback method to load the target scene after the loading scene is displayed.
    /// </summary>
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString()); // Load the target scene
    }
}