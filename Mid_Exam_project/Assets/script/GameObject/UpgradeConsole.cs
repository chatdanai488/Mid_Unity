using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeConsole : MonoBehaviour
{
    private GameObject UpgradeScreenObject;
    private CanvasGroup UpgradeScene;
    private Collider2D collider2D;
    public ContactFilter2D contactFilter;
    private List<Collider2D> colliders = new List<Collider2D>(1);
    public CanvasGroup E;
    private void CallUpgradeScreen()
    {
        UpgradeUIScript UpgradeUIScriptScript = UpgradeScene.GetComponent<UpgradeUIScript>();
        UpgradeUIScriptScript.ResetButton();
        UpgradeUIScriptScript.InitializeLevel();
        Time.timeScale = 0;

        UpgradeScene.alpha = 1;
        Canvas UpgradScene = UpgradeScene.GetComponent<Canvas>();
        UpgradScene.sortingOrder = 100;
    }
    // Start is called before the first frame update
    private void Start()
    {
        UpgradeScreenObject = GameObject.Find("UpgradeScreen");
        UpgradeScene = UpgradeScreenObject.GetComponent<CanvasGroup>();
        collider2D = GetComponent<Collider2D>();

        collider2D.OverlapCollider(contactFilter, colliders);


        if (collider2D.OverlapCollider(contactFilter, colliders) == 0)
        {
            E.alpha = 0;
            if (Input.GetKeyDown(KeyCode.E)) { }
        }
        else
        {
            E.alpha = 1;
            GetkeyUpgrade();
        }
    }
    private void Update()
    {
        collider2D.OverlapCollider(contactFilter, colliders);
        
        
        if(collider2D.OverlapCollider(contactFilter, colliders) == 0)
        {
            E.alpha = 0;
            if (Input.GetKeyDown(KeyCode.E)) {  }
        }
        else
        {
            E.alpha = 1;
            GetkeyUpgrade();
        }
    }
    private void GetkeyUpgrade()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CallUpgradeScreen();
        }
    }
}
