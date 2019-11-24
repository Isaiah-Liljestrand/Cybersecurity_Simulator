using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerControl : MonoBehaviour
{
    public DiseaseType Disease;
    public float MinAttackWait;
    public float MaxAttackWait;
    public float AttackWait;
    public bool Hidden;
    public int Index;
    private float PassedTime;
    private GameControl Control;
    private NetworkControl Net;

    private bool Paused;

    private GameObject ExclamationMark;

    public bool CanInvestigate;

    private void Start()
    {
        Control = GameObject.Find("Control").GetComponent<GameControl>();
        Net = GameObject.Find("Control").GetComponent<NetworkControl>();
    }

    public void Infected(DiseaseType Disease, float MinAttackWait, float MaxAttackWait, bool Hidden)
    {
        this.Disease = Disease;
        this.MinAttackWait = MinAttackWait;
        this.MaxAttackWait = MaxAttackWait;
        this.AttackWait = Random.Range(MinAttackWait, MaxAttackWait);
        this.Hidden = Hidden;
        PassedTime = 0;
        Net.set_infection_status(gameObject, Disease);
    }

    public void Clean()
    {
        Disease = DiseaseType.Clean;
        Hidden = false;
    }

    private void Update()
    {
        if (!Paused)
        {
            if (Disease != DiseaseType.Clean && Disease != DiseaseType.DOS)
            {
                PassedTime += Time.deltaTime;
                if (PassedTime > AttackWait)
                {
                    AttackWait = Random.Range(MinAttackWait, MaxAttackWait);
                    Attack();
                    PassedTime = 0;
                }
            }
        }
    }

    public void Pause()
    {
        Paused = true;
    }

    public void Resume()
    {
        Paused = false;
    }

    //Attack connected nodes
    private void Attack()
    {
        List<int> ConnectedIndices = new List<int>();
        for (int i = 0; i < 16; i++)
        {
            if (Net.Connections[Index, i] == 1)
            {
                ConnectedIndices.Add(i);
            }
        }
        List<int> CleanComps = new List<int>();
        foreach(int Index in ConnectedIndices)
        {
            ComputerControl OtherComp = Control.DeskObjects[Index].GetComponent<ComputerControl>();
            if (OtherComp.Disease == DiseaseType.Clean)
                CleanComps.Add(Index);
        }
        if (CleanComps.Count > 0)
        {
            int RandomIndex = Random.Range(0, CleanComps.Count - 1);
            int ChosenIndex = CleanComps[RandomIndex];
            EmployeeControl Employee = Control.EmployeeObjs[ChosenIndex].GetComponent<EmployeeControl>();
            if (!Employee.PassedResistanceCheck(Disease))
            {
                Control.DeskObjects[ChosenIndex].GetComponent<ComputerControl>().Infected(Disease, MinAttackWait, MaxAttackWait, Hidden);
                Employee.Infected(Disease, true, MinAttackWait, MaxAttackWait, Hidden);
            }
        }
    }

    public void CreateIssue()
    {
        ExclamationMark = Instantiate(Control.ExclamationPrefab, this.transform);
        Hidden = false;
        CanInvestigate = true;
    }

    public void SolveIssue()
    {
        Destroy(ExclamationMark);
        ExclamationMark = null;
        CanInvestigate = false;
    }
}
