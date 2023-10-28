using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinScript : MonoBehaviour
{
    private int CoinValue;
    private Rigidbody2D rb;
    public void GetCoinValue(int value)
    {
        CoinValue = value;
    }

    public void SendCoin()
    {
        GameObject StatsManagerObject = GameObject.Find("StatsManager");
        StatsManager StatsManagerScript = StatsManagerObject.GetComponent<StatsManager>();
       
        StatsManagerScript.UpdateCoinValue(CoinValue);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float RandomDistance = Random.Range(-30, 31);
        rb.velocity = new Vector2(RandomDistance / 10, 1f);

    }
}
