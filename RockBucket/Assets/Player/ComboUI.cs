using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    [SerializeField] private Color successColor;
    [SerializeField] private Color missedColor;
    [Space(10)]
    [SerializeField] TextMeshProUGUI comboText;

    private void Start()
    {
        ComboController.OnComboUpdated += ComboController_OnComboUpdated;
        ComboController_OnComboUpdated(string.Empty);
    }

    private void OnDestroy()
    {
        ComboController.OnComboUpdated -= ComboController_OnComboUpdated;
    }

    private void ComboController_OnComboUpdated(string combo)
    {
        comboText.SetText(combo);
    }
}