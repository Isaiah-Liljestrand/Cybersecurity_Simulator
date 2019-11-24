using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerControl : MonoBehaviour
{
    public DiseaseType Disease;
    public float AttackWait;
    public bool Hidden;
    public int Index;
    private float PassedTime;
    private GameControl Control;
    private NetworkControl Net;

    private void Start()
    {
        Control = GameObject.Find("Control").GetComponent<GameControl>();
        Net = GameObject.Find("Control").GetComponent<NetworkControl>();
    }

    public void Infected(DiseaseType Disease, float AttackWait, bool Hidden)
    {
        this.Disease = Disease;
        this.AttackWait = AttackWait;
        this.Hidden = Hidden;
        PassedTime = 0;
        Net.set_infection_status(gameObject, Disease);
    }

    private void Update()
    {
        if (Disease != DiseaseType.Clean && Disease != DiseaseType.DOS)
        {
            PassedTime += Time.deltaTime;
            if (PassedTime > AttackWait)
            {
                Attack();
                PassedTime = 0;
            }
        }
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
                Control.DeskObjects[ChosenIndex].GetComponent<ComputerControl>().Infected(Disease, AttackWait, Hidden);
                Employee.Infected(Disease, true);
            }
        }
    }
}
