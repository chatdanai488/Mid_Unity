using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{
    
    public TextMeshProUGUI CoinCount;


    public void UpdateCoinValue(int Value)
    {
        CoinCount.text = Value.ToString();

    }
    
}
