using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private int turns;

    // Start is called before the first frame update
    void Start()
    {
        NC = GetComponent<NetworkControl>();
        DC = GetComponent<DiseaseControl>();
        TC = GetComponent<TimeLineControl>();
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

    public void ComputerFix(bool isfixed, Disease disease)
    {
        if (isfixed)
            DC.CleanVirus(disease);
        PassTurn(1);
    }


    public void PlayerComputerClicked(GameObject obj)
    {
        NC.ShowNetwork();
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
