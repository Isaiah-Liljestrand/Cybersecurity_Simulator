﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject PlayerObj;
    public List<EmployeeDeskPair> employees;
    public List<GameObject> BreakObjects;
    


    private bool Paused;
    private NetworkControl NC;

    private int turns;

    // Start is called before the first frame update
    void Start()
    {
        turns = 0;
    }

    public void PassTurn(int turnspassed)
    {
        turns++;
        //Tell everybody the shit that happens

    }

    public void PersonClicked(GameObject obj)
    {
        
    }

    public void ComputerClicked(GameObject obj)
    {
        
    }

    public void PlayerComputerClicked(GameObject obj)
    {

    }

    public bool IsPaused()
    {
        return Paused;
    }

    public void Pause()
    {
        Paused = true;
        foreach (EmployeeDeskPair pair in employees)
        {
            pair.Pause();
        }
    }

    public void Resume()
    {
        Paused = false;
        foreach (EmployeeDeskPair pair in employees)
        {
            pair.Resume();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
