using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarUI : MonoBehaviour
{
    [SerializeField] RectTransform artParent;
    [SerializeField] RectTransform arrow;
    [SerializeField] RectTransform powerBar;
    [Space(10)]
    [SerializeField] RockThrowing rockThrowing;

    private void Start()
    {
        rockThrowing.ThrowStart += throwStart;
        rockThrowing.ThrowMiddle += throwMiddle;
        rockThrowing.ThrowEnd += throwEnd;

        throwEnd();
    }

    private void OnDisable()
    {
        rockThrowing.ThrowStart -= throwStart;
        rockThrowing.ThrowMiddle -= throwMiddle;
        rockThrowing.ThrowEnd -= throwEnd;
    }

    private void throwStart()
    {
        artParent.gameObject.SetActive(true);
    }

    private void throwMiddle(float power)
    {
        SetPower(power);
    }

    private void throwEnd()
    {
        artParent.gameObject.SetActive(false);
    }

    private void SetPower(float t)
    {
        float topY = powerBar.anchoredPosition.y + powerBar.sizeDelta.y / 2;
        float botY = powerBar.anchoredPosition.y - powerBar.sizeDelta.y / 2;

        float pos = Mathf.Lerp(botY, topY, t);

        Vector2 outputPos = new Vector2(arrow.anchoredPosition.x, pos);
        arrow.anchoredPosition = outputPos;
    }
}