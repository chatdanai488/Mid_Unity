using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static int CoinCount;

    public void UpdateCoinValue(int value)
    {
        CoinCount += value;
        PlayerPrefs.SetInt("CoinCount", CoinCount);
        SendValue();
    }
    private void SendValue()
    {
        GameObject PlayerUIObject = GameObject.Find("PlayerUI");
        PlayerUIScript PlayerUIScriptScript = PlayerUIObject.GetComponent<PlayerUIScript>();
        PlayerUIScriptScript.UpdateCoinValue(CoinCount);
    }

    private void GetCoinValue()
    {
        PlayerPrefs.GetInt("CoinCount");
    }
    void Start()
    {
        GetCoinValue();
        AfterUpgrade();
    }
    public void AfterUpgrade()
    {
        CoinCount = PlayerPrefs.GetInt("CoinCount");
        SendValue() ;
    }
   
}
