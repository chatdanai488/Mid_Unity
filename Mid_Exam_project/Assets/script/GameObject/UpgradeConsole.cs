using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeConsole : MonoBehaviour
{
    public CanvasGroup UpgradeScene;
    private Collider2D collider2D;
    private ContactFilter2D contactFilter;
    private List<Collider2D> colliders = new List<Collider2D>(1);
    public CanvasGroup E;
    private void CallUpgradeScreen()
    {
        UpgradeUIScript UpgradeUIScriptScript = UpgradeScene.GetComponent<UpgradeUIScript>();
        UpgradeUIScriptScript.ResetButton();
        Time.timeScale = 0;
        UpgradeScene.alpha = 1;
        Canvas UpgradScene = UpgradeScene.GetComponent<Canvas>();
        UpgradScene.sortingOrder = 6;
    }
    // Start is called before the first frame update
    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }
    private void Update()
    {
        collider2D.OverlapCollider(contactFilter, colliders);
        foreach(var o in colliders)
        {
            E.alpha = 1;
            GetkeyUpgrade();
        }
        
        if(collider2D.OverlapCollider(contactFilter, colliders) == 0)
        {
            E.alpha = 0;
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
