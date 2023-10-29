using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static int CoinCount;
    private static int HealthLevel;
    private static int BulletCountLevel;
    private static int AttackLevel;
    private static int AttackSpeedLevel;
    private static int SpeedLevel;
    private static int JumpPowerLevel;

    private void Awake()
    {
        GetPlayerAttribute();
    }

    public void UpdatePlayerAttribute()
    {
        GameObject PlayerObject = GameObject.Find("Player");
        PlayerMovement PlayerMovementScript = PlayerObject.GetComponent<PlayerMovement>();
        if (HealthLevel == null) { HealthLevel = 0; }
        if (BulletCountLevel == null) { BulletCountLevel = 0; }
        if (AttackLevel == null) { AttackLevel = 0; }
        if (AttackSpeedLevel == null) { AttackSpeedLevel = 0; }
        if (SpeedLevel == null) { SpeedLevel = 0; }
        if (JumpPowerLevel == null) { JumpPowerLevel = 0; }
        PlayerMovementScript.InitializeAttribute(HealthLevel,AttackLevel,BulletCountLevel,AttackSpeedLevel, SpeedLevel, JumpPowerLevel);

    }
    public void UpgradePlayerAttribute(int HealthLevel, int AttackLevel, int BulletCountLevel, int AttackSpeedLevel, int SpeedLevel, int JumpPowerLevel)
    {
        PlayerPrefs.SetInt("HealthLevel", HealthLevel);
        PlayerPrefs.SetInt("AttackLevel", AttackLevel);
        PlayerPrefs.SetInt("BulletCountLevel", BulletCountLevel);
        PlayerPrefs.SetInt("AttackSpeedLevel", AttackSpeedLevel);
        PlayerPrefs.SetInt("SpeedLevel", SpeedLevel);
        PlayerPrefs.SetInt("JumpPowerLevel", JumpPowerLevel);
        GetPlayerAttribute();
    }
    private void GetPlayerAttribute()
    {
        HealthLevel = PlayerPrefs.GetInt("HealthLevel");
        AttackLevel = PlayerPrefs.GetInt("AttackLevel");
        BulletCountLevel = PlayerPrefs.GetInt("BulletCountLevel");
        AttackSpeedLevel =  PlayerPrefs.GetInt("AttackSpeedLevel");
        SpeedLevel =  PlayerPrefs.GetInt("SpeedLevel");
        JumpPowerLevel = PlayerPrefs.GetInt("JumpPowerLevel");
        UpdatePlayerAttribute();
    }

    public int GetHealthLevel()
    {
        return HealthLevel;
    }
    public int GetAttackLevel()
    {
        return AttackLevel;
    }
    public int GetSpeedLevel()
    {
        return SpeedLevel;
    }
    public int GetJumpPowerLevel()
    {
        return JumpPowerLevel;
    }
    public int GetBulletCountLevel()
    {
        return BulletCountLevel;
    }
    public int GetAttackSpeedLevel()
    {
        return AttackSpeedLevel;
    }
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
        CoinCount = PlayerPrefs.GetInt("CoinCount");
    }
    void Start()
    {
        GetCoinValue();
    }

   
}
