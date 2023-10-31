using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScenesManager Instances;
    private void Awake()
    {
        Instances = this;
    }
    // Start is called before the first frame update

    public enum Scene
    {
        MainMenu,
        LevelSelect,
        Level1,
        Level2
    }
    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.Level1.ToString());
    }
    public void LoadLevelSelection()
    {
        SceneManager.LoadScene(Scene.LevelSelect.ToString());
    }
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
