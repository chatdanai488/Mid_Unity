using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button NewGameButton;
    [SerializeField] Button LevelSelectionButton;
    public GameObject ScenesManagerObject;

    private void StartNewGame()
    {
        ScenesManager ScenesManagerScript = ScenesManagerObject.GetComponent<ScenesManager>();
        ScenesManagerScript.LevelSelect(1);
    }
    private void LevelSelect()
    {
        ScenesManager ScenesManagerScript = ScenesManagerObject.GetComponent<ScenesManager>();
        ScenesManagerScript.LoadLevelSelection();

    }
    void Start()
    {
        NewGameButton.onClick.AddListener(StartNewGame);
        LevelSelectionButton.onClick.AddListener(LevelSelect);
    }

}
