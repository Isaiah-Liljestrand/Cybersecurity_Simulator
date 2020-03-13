using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedObject : MonoBehaviour
{
    private ClickManager control;
    private GameObject standlocation;
    public ObjectType type;

    private void Start()
    {
        if (transform.Find("StandingLocation"))
            standlocation = transform.Find("StandingLocation").gameObject;
        else
            standlocation = gameObject;
        control = GameObject.Find("Control").GetComponent<ClickManager>();
    }

    private void OnMouseUp()
    {
        if (standlocation)
        {
            switch (type) {
                case ObjectType.Computer:
                    control.ComputerClicked(standlocation);
                    break;
                case ObjectType.Other:
                    control.ObjectClicked(standlocation);
                    break;
                case ObjectType.Person:
                    control.PersonClicked(standlocation);
                    break;
                case ObjectType.PlayerComputer:
                    control.PlayerComputerClicked(standlocation);
                    break;
            }
        }
    }
}
