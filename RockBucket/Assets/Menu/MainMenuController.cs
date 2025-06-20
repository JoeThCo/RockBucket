using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private TextMeshProUGUI companyText;

    private void Start()
    {
        versionText.SetText(Application.version);
        titleText.SetText(Application.productName);
        companyText.SetText(Application.companyName);
    }
}