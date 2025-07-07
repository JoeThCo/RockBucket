using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonJuice : MonoBehaviour
{
    private Button button;
    private bool canJuice = true;
    private void Start()
    {
        button = GetComponent<Button>();
        canJuice = button.interactable;
    }

    public void MouseEnter()
    {
        if (!canJuice) return;
        transform.localScale = Vector3.one * 1.15f;
    }

    public void MouseExit()
    {
        if (!canJuice) return;
        transform.localScale = Vector3.one;
    }
}