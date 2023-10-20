using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>(); 
    
    }
    public void SetBar(float Health, int MaxHealth)
    {
        slider.maxValue = MaxHealth;
        slider.value = Health;
    }
    
}
