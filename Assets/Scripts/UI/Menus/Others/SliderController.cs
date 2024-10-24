using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderText = null;
    
    private float maxSliderAmount = 100f;

    public void SliderChange(float value)
    {
        int localValue = (int)(value * maxSliderAmount);
        sliderText.text = localValue.ToString() + "%";
    }
}
