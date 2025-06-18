using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindDirectionUI : MonoBehaviour
{
    [SerializeField] private Gradient powerGradient;
    [SerializeField] private Image arrowUI;
    [SerializeField] private TextMeshProUGUI windSpeedText;
    [Space(10)]
    [SerializeField] private Transform player;
    [SerializeField] private WindDirection windDireciton;

    private void Awake()
    {
        windDireciton.WindChangedUI += windChangedUI;
    }

    private void OnDisable()
    {
        windDireciton.WindChangedUI -= windChangedUI;
    }

    private void FixedUpdate()
    {
        float windAngle = Mathf.Atan2(windDireciton.Direction.x, windDireciton.Direction.z) * Mathf.Rad2Deg;
        float playerAngle = player.eulerAngles.y;
        float angleDifference = Mathf.DeltaAngle(playerAngle, windAngle);

        arrowUI.rectTransform.rotation = Quaternion.Euler(0, 0, -angleDifference + 90);
    }

    private void windChangedUI()
    {
        arrowUI.color = powerGradient.Evaluate(windDireciton.WindSpeed01);
        windSpeedText.SetText(windDireciton.WindSpeedString);
    }
}