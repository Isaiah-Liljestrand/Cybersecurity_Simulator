using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidebookUIControl : MonoBehaviour
{
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void TurnOn()
    {
        canvas.enabled = true;
    }

    public void TurnOff()
    {
        canvas.enabled = false;
    }
}
