using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNextGameBuitton : MonoBehaviour
{
    public GameObject ScenesManagerObject;
    private ScenesManager ScenesManagerScript;
    [SerializeField] public Button GoToLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        ScenesManagerScript = ScenesManagerObject.GetComponent<ScenesManager>();
        GoToLevelButton.onClick.AddListener(ScenesManagerScript.TutorialSceneLoad);
    }

    
}
