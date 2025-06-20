using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonJuice : MonoBehaviour
{
    public void MouseEnter()
    {
        transform.localScale = Vector3.one * 1.15f;
    }

    public void MouseExit()
    {
        transform.localScale = Vector3.one;
    }
}