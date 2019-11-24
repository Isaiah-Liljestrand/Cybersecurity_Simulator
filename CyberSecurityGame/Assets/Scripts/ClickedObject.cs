using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedObject : MonoBehaviour
{
    private GameControl control;
    private GameObject standlocation;

    private void Start()
    {
        standlocation = transform.Find("StandingLocation").gameObject;
        control = GameObject.Find("Control").GetComponent<GameControl>();
    }

    private void OnMouseDown()
    {
        if (standlocation)
            control.ObjectClicked(standlocation);
    }
}
