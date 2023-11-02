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
        TutorialScreen,
        Level1,
        Level2,
        LevelEnd
    }
    public void LoadNewGame()
    {
        int value = 1;
        PlayerPrefs.SetInt("SelectedLevel", value);
        SceneManager.LoadScene(Scene.TutorialScreen.ToString());
    }
    public void LoadLevelSelection()
    {
        SceneManager.LoadScene(Scene.LevelSelect.ToString());
    }
    public void LevelSelect(int level)
    {
        PlayerPrefs.SetInt("SelectedLevel", level);
        SceneManager.LoadScene(Scene.TutorialScreen.ToString());
    }
    public void TutorialSceneLoad()
    {
        int Level = PlayerPrefs.GetInt("SelectedLevel");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Level);
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
