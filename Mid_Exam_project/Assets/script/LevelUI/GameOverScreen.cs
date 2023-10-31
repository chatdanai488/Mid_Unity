using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup DeadScreen;
    private Canvas DeadScreenCanvas;
    private GameObject ScenesManagerObject;
    private ScenesManager ScenesManagerScript;

    [SerializeField] public Button DeadRetryButton;
    [SerializeField] public Button DeadHomeButton;
    private void Awake()
    {
        DeadScreenCanvas = GetComponent<Canvas>();
        DeadScreenCanvas.sortingOrder = -1;
        DeadScreen = GetComponent<CanvasGroup>();
        DeadScreen.alpha = 0;

        ScenesManagerObject = GameObject.Find("ScenesManager");
        ScenesManagerScript = ScenesManagerObject.GetComponent<ScenesManager>();
    }
    void Start()
    {
        DeadRetryButton.onClick.AddListener(ScenesManagerScript.ReloadLevel);
        DeadHomeButton.onClick.AddListener(ScenesManagerScript.LoadMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
