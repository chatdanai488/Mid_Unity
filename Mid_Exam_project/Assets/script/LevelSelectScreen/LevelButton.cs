using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Button LevelButtonObject;
    public TextMeshProUGUI LevelSelect;
    public GameObject ScenesManagerObject;
    private void LoadLevel()
    {
        ScenesManager ScenesManagerScript = ScenesManagerObject.GetComponent<ScenesManager>();
        ScenesManagerScript.LevelSelect(Int32.Parse(LevelSelect.text));
    }
    void Update()
    {
        LevelButtonObject.onClick.AddListener(LoadLevel);
    }
}
