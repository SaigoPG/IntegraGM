using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinsUI : FadeMenu
{
    [SerializeField] private TextMeshProUGUI coinText;
    public void ActualizeUI(int newAmount)
    {
        coinText.text = newAmount.ToString() + "$";
    }
}
