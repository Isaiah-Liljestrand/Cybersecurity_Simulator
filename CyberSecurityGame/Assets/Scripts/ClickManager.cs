﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public GameControl gamecontrol;
    public GameObject TargetObj;
    public float taptime;
    public float ActivationDistance; //Distance at which the player can investigate/talk.

    private float holdingtime;
    private bool holdingclicked;

    // Update is called once per frame
    void Update()
    {
        if (!gamecontrol.IsPaused())
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdingclicked = true;
                holdingtime = 0;
            }
            if (holdingclicked)
            {
                holdingtime += Time.deltaTime;
                if (Input.GetMouseButtonUp(0))
                {
                    holdingclicked = false;
                    if (holdingtime <= taptime)
                        Clicked();
                }
            }
        }
    }

    private void Clicked()
    {
        //Check to see if the floor was clicked
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                TargetObj.SetActive(true);
                TargetObj.transform.position = hit.point;
                gamecontrol.PlayerObj.GetComponent<NavigateTo>().GoToPosition(hit.point, 1);
            }
        }
    }

    public void PersonClicked(GameObject obj)
    {
        if (Vector3.Distance(gamecontrol.PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {

        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void ComputerClicked(GameObject obj)
    {
        if (Vector3.Distance(gamecontrol.PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {

        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void PlayerComputerClicked(GameObject obj)
    {
        if (Vector3.Distance(gamecontrol.PlayerObj.transform.position, obj.transform.position) < ActivationDistance)
        {

        }
        else
        {
            ObjectClicked(obj);
        }
    }

    public void ObjectClicked(GameObject obj)
    {
        if (!gamecontrol.IsPaused())
        {
            TargetObj.SetActive(true);
            TargetObj.transform.position = obj.transform.position;
            gamecontrol.PlayerObj.GetComponent<NavigateTo>().GoToPosition(obj.transform.position, 1);
        }
    }
}