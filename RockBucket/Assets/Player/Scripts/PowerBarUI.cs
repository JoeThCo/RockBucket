using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerBarUI : MonoBehaviour
{
    [SerializeField] RectTransform artParent;
    [SerializeField] RectTransform arrow;
    [SerializeField] RectTransform powerBar;
    [Space(10)]
    [SerializeField] RockThrowing rockThrowing;

    private void Start()
    {
        rockThrowing.ThrowStart += RockThrowing_ThrowStart;
        rockThrowing.ThrowMiddle += RockThrowing_ThrowMiddle;
        rockThrowing.ThrowEnd += RockThrowing_ThrowEnd;

        RockThrowing_ThrowEnd();
    }

    private void OnDestroy()
    {
        rockThrowing.ThrowStart -= RockThrowing_ThrowStart;
        rockThrowing.ThrowMiddle -= RockThrowing_ThrowMiddle;
        rockThrowing.ThrowEnd -= RockThrowing_ThrowEnd;
    }

    private void RockThrowing_ThrowStart()
    {
        artParent.gameObject.SetActive(true);
    }

    private void RockThrowing_ThrowMiddle()
    {
        SetPower(rockThrowing.Power);
    }

    private void RockThrowing_ThrowEnd()
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