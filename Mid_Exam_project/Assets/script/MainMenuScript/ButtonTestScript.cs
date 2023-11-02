using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ScenesManagerObject;
    private ScenesManager ScenesManagerScript;
    [SerializeField] public Button StartButton;
    void Start()
    {
        ScenesManagerScript = ScenesManagerObject.GetComponent<ScenesManager>();
        StartButton.onClick.AddListener(ScenesManagerScript.LoadNewGame);
    }

    
}
