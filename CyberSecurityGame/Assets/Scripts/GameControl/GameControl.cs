using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UIState
{
    InGame,
    Network,
    Dialogue,
    Computer
}

public class GameControl : MonoBehaviour
{
    public GameObject PlayerObj;
    public List<EmployeeDeskPair> employees;
    public List<GameObject> BreakObjects;

    public ChecklistUIControl checklistui;
    public GuidebookUIControl guidebookui;
    
    private bool Paused;
    private NetworkControl NC;
    private DiseaseControl DC;
    private TimeLineControl TC;
    private EducationControl EC;

    public UIState uistate;

    private int turns;

    // Start is called before the first frame update
    void Start()
    {
        NC = GetComponent<NetworkControl>();
        DC = GetComponent<DiseaseControl>();
        TC = GetComponent<TimeLineControl>();
        EC = GetComponent<EducationControl>();
        uistate = UIState.InGame;
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

    public void ComputerFix(bool isfixed, int computer)
    {
        if (isfixed)
            DC.CleanComputer(computer);
        PassTurn(1);
    }


    public void PlayerComputerClicked(GameObject obj)
    {
        uistate = UIState.Network;
        Pause();
        NC.ShowNetwork();
    }

    public void LeaveNetwork()
    {
        uistate = UIState.InGame;
        Resume();
        NC.HideNetwork();
    }

    public void ChecklistClicked()
    {
        checklistui.TurnOnChecklist();
    }

    public void GuidebookClicked()
    {
        guidebookui.TurnOn();
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
