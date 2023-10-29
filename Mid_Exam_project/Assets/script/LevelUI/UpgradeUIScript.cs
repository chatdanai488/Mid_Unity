using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeUIScript : MonoBehaviour
{
    public TextMeshProUGUI HealthLevel;
    public TextMeshProUGUI AttackLevel;
    public TextMeshProUGUI AttackSpeedLevel;
    public TextMeshProUGUI BulletCountLevel;
    public TextMeshProUGUI SpeedLevel;
    public TextMeshProUGUI JumpPowerLevel;

    public TextMeshProUGUI CoinCount;

    public TextMeshProUGUI HealthUpgradeCost;
    public TextMeshProUGUI AttackUpgradeCost;
    public TextMeshProUGUI AttackSpeedUpgradeCost;
    public TextMeshProUGUI BulletCountUpgradeCost;
    public TextMeshProUGUI SpeedUpgradeCost;
    public TextMeshProUGUI JumpPowerUpgradeCost;

    [SerializeField] public Button HealthUpgradeButton;
    [SerializeField] public Button AttackUpgradeButton;
    [SerializeField] public Button AttackSpeedUpgradeButton;
    [SerializeField] public Button BulletCountUpgradeButton;
    [SerializeField] public Button SpeedUpgradeButton;
    [SerializeField] public Button JumpPowerUpgradeButton;
    [SerializeField] public Button QuitButton;
    // Start is called before the first frame update
    private int HealthLevelCost;
    private int AttackLevelCost;
    private int BulletCountLevelCost;
    private int SpeedLevelCost;
    private int JumpPowerLevelCost;
    private int CoinCountCost;
    private int AttackSpeedLevelCost;

    private int HealthLevelInt;
    private int AttackLevelInt;
    private int BulletCountLevelInt;
    private int SpeedLevelInt;
    private int JumpPowerLevelInt;
    private int AttackSpeedLevelInt;

    public CanvasGroup UpgradeScene;
    private void Awake()
    {
        InitializeLevel();
        GetPlayerAttribute();
        ResetButton();
    }
    public void ResetButton()
    {
        HealthUpgradeButton.enabled = true;
        AttackUpgradeButton.enabled = true;
        AttackSpeedUpgradeButton.enabled = true;
        BulletCountUpgradeButton.enabled = true;
        SpeedUpgradeButton.enabled = true;
        JumpPowerUpgradeButton.enabled = true;
    }
    private void InitializeLevel()
    {
        GameObject StatsManagerObject = GameObject.Find("StatsManager");
        StatsManager StatsManagerScript = StatsManagerObject.GetComponent<StatsManager>();

        HealthLevel.text = PlayerPrefs.GetInt("HealthLevel").ToString();
        AttackLevel.text = PlayerPrefs.GetInt("AttackLevel").ToString();
        AttackSpeedLevel.text = PlayerPrefs.GetInt("AttackSpeedLevel").ToString();
        BulletCountLevel.text = PlayerPrefs.GetInt("BulletCountLevel").ToString();
        SpeedLevel.text = PlayerPrefs.GetInt("SpeedLevel").ToString();
        JumpPowerLevel.text = PlayerPrefs.GetInt("JumpPowerLevel").ToString();


        HealthLevelCost = 5 + (2 * PlayerPrefs.GetInt("HealthLevel"));
        AttackLevelCost = 5 + (2 * PlayerPrefs.GetInt("AttackLevel"));
        AttackSpeedLevelCost = 5 + (2 * PlayerPrefs.GetInt("AttackSpeedLevel"));
        BulletCountLevelCost = 5 + (2 * PlayerPrefs.GetInt("BulletCountLevel"));
        SpeedLevelCost = 5 + (2 * PlayerPrefs.GetInt("SpeedLevel"));
        JumpPowerLevelCost = 5 + (2 * PlayerPrefs.GetInt("JumpPowerLevel"));

        CoinCountCost = PlayerPrefs.GetInt("CoinCount");

        HealthUpgradeCost.text = HealthLevelCost.ToString();
        AttackUpgradeCost.text = AttackLevelCost.ToString();
        AttackSpeedUpgradeCost.text = AttackSpeedLevelCost.ToString();
        BulletCountUpgradeCost.text = BulletCountLevelCost.ToString();
        SpeedUpgradeCost.text = SpeedLevelCost.ToString();
        JumpPowerUpgradeCost.text = SpeedLevelCost.ToString();

        CoinCount.text = CoinCountCost.ToString();
    }
    private void GetPlayerAttribute()
    {
        HealthLevelInt = PlayerPrefs.GetInt("HealthLevel");
        AttackLevelInt = PlayerPrefs.GetInt("AttackLevel");
        BulletCountLevelInt = PlayerPrefs.GetInt("BulletCountLevel");
        AttackSpeedLevelInt = PlayerPrefs.GetInt("AttackSpeedLevel");
        SpeedLevelInt = PlayerPrefs.GetInt("SpeedLevel");
        JumpPowerLevelInt = PlayerPrefs.GetInt("JumpPowerLevel");
        UpdatePlayerAttribute();
    }
    private void UpdatePlayerAttribute()
    {
        if (HealthLevelInt == null) { HealthLevelInt = 0; }
        if (BulletCountLevelInt == null) { BulletCountLevelInt = 0; }
        if (AttackLevelInt == null) { AttackLevelInt = 0; }
        if (AttackSpeedLevelInt == null) { AttackSpeedLevelInt = 0; }
        if (SpeedLevelInt == null) { SpeedLevelInt = 0; }
        if (JumpPowerLevelInt == null) { JumpPowerLevelInt = 0; }
        GameObject PlayerObject = GameObject.Find("Player");
        PlayerMovement PlayerMovementScript = PlayerObject.GetComponent<PlayerMovement>();
        PlayerMovementScript.InitializeAttribute(HealthLevelInt, AttackLevelInt, BulletCountLevelInt, AttackSpeedLevelInt, SpeedLevelInt, JumpPowerLevelInt);
    }
    private void UpgradeHealth()
    {
        CoinCountCost -= HealthLevelCost;
        CoinCount.text = CoinCountCost.ToString();
        PlayerPrefs.SetInt("CoinCount", CoinCountCost);

        HealthLevelInt += 1;
        PlayerPrefs.SetInt("HealthLevel", HealthLevelInt);
        HealthLevel.text = HealthLevelInt.ToString();

        HealthLevelCost = 5 + (2 * PlayerPrefs.GetInt("HealthLevel"));
        HealthUpgradeCost.text = HealthLevelCost.ToString();
    }
    private void UpgradeAttack()
    {
        CoinCountCost -= AttackLevelCost;
        CoinCount.text = CoinCountCost.ToString();
        PlayerPrefs.SetInt("CoinCount", CoinCountCost);

        AttackLevelInt += 1;
        PlayerPrefs.SetInt("AttackLevel", AttackLevelInt);
        AttackLevel.text = AttackLevelInt.ToString();

        AttackLevelCost = 5 + (2 * PlayerPrefs.GetInt("AttackLevel"));
        AttackUpgradeCost.text = AttackLevelCost.ToString();
    }
    private void UpgradeAttackSpeed()
    {
        CoinCountCost -= AttackSpeedLevelCost;
        CoinCount.text = CoinCountCost.ToString();
        PlayerPrefs.SetInt("CoinCount", CoinCountCost);

        AttackSpeedLevelInt += 1;
        PlayerPrefs.SetInt("AttackSpeedLevel", AttackSpeedLevelInt);
        AttackSpeedLevel.text = AttackSpeedLevelInt.ToString();

        AttackSpeedLevelCost = 5 + (2 * PlayerPrefs.GetInt("AttackSpeedLevel"));
        AttackSpeedUpgradeCost.text = AttackSpeedLevelCost.ToString();
    }
    private void UpgradeSpeed()
    {
        CoinCountCost -= SpeedLevelCost;
        CoinCount.text = CoinCountCost.ToString();
        PlayerPrefs.SetInt("CoinCount", CoinCountCost);

        SpeedLevelInt += 1;
        PlayerPrefs.SetInt("SpeedLevel", SpeedLevelInt);
        SpeedLevel.text = SpeedLevelInt.ToString();

        SpeedLevelCost = 5 + (2 * PlayerPrefs.GetInt("SpeedLevel"));
        SpeedUpgradeCost.text = SpeedLevelCost.ToString();
    }
    private void UpgradeJumpPower()
    {
        CoinCountCost -= JumpPowerLevelCost;
        CoinCount.text = CoinCountCost.ToString();
        PlayerPrefs.SetInt("CoinCount", CoinCountCost);

        JumpPowerLevelInt += 1;
        PlayerPrefs.SetInt("JumpPowerLevel", JumpPowerLevelInt);
        JumpPowerLevel.text = JumpPowerLevelInt.ToString();

        JumpPowerLevelCost = 5 + (2 * PlayerPrefs.GetInt("JumpPowerLevel"));
        JumpPowerUpgradeCost.text = JumpPowerLevelCost.ToString();
    }
    private void UpgradeBulletCount()
    {
        CoinCountCost -= BulletCountLevelCost;
        CoinCount.text = CoinCountCost.ToString();
        PlayerPrefs.SetInt("CoinCount", CoinCountCost);

        BulletCountLevelInt += 1;
        PlayerPrefs.SetInt("BulletCountLevel", BulletCountLevelInt);
        BulletCountLevel.text = BulletCountLevelInt.ToString();

        BulletCountLevelCost = 5 + (2 * PlayerPrefs.GetInt("BulletCountLevel"));
        BulletCountUpgradeCost.text = BulletCountLevelCost.ToString();
    }
    private void CheckCanUpgrade()
    {
        if (HealthLevelCost > CoinCountCost)
        {
            HealthUpgradeButton.interactable = false;
        }
        if (AttackLevelCost > CoinCountCost)
        {
            AttackUpgradeButton.interactable = false;
        }
        if (AttackSpeedLevelCost > CoinCountCost)
        {
            AttackSpeedUpgradeButton.interactable = false;
        }
        if (BulletCountLevelCost > CoinCountCost)
        {
            BulletCountUpgradeButton.interactable = false;
        }
        if (SpeedLevelCost > CoinCountCost)
        {
            SpeedUpgradeButton.interactable = false;
        }
        if (JumpPowerLevelCost > CoinCountCost)
        {
            JumpPowerUpgradeButton.interactable = false;
        }
    }
    private void Quit()
    {
        Time.timeScale = 1;
        UpdatePlayerAttribute();

        GameObject StatsManagerObject = GameObject.Find("StatsManager");
        StatsManager StatsManagerScript = StatsManagerObject.GetComponent<StatsManager>();
        StatsManagerScript.AfterUpgrade();

        UpgradeScene.alpha = 0;
        Canvas UpgradScene = UpgradeScene.GetComponent<Canvas>();
        UpgradScene.sortingOrder = -1;
    }

    void Start()
    {
        AttackUpgradeButton.onClick.AddListener(UpgradeAttack);
        HealthUpgradeButton.onClick.AddListener(UpgradeHealth);
        BulletCountUpgradeButton.onClick.AddListener(UpgradeBulletCount);
        AttackSpeedUpgradeButton.onClick.AddListener(UpgradeAttackSpeed);
        SpeedUpgradeButton.onClick.AddListener(UpgradeSpeed);
        JumpPowerUpgradeButton.onClick.AddListener(UpgradeJumpPower);
        QuitButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanUpgrade();
    }
}