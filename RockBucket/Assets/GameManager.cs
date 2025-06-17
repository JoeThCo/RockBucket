using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;
    private void Start()
    {
        SoundEffectController.Load();

        versionText.SetText(Application.version);
    }
}