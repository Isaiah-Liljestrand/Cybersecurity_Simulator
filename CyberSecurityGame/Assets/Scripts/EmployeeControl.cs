using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeControl : MonoBehaviour
{
    public String Name;
    public int ResistPhish;
    public int ResistPassword;
    public int ResistUpload;
    public DiseaseType DiseaseCode;
    public bool Mystery;

    public GameObject Office;

    public float BreakTimeMin;
    public float BreakTimeMax;
    public float WorkTimeMin;
    public float WorkTimeMax;

    private float ChosenTime;
    private float PassedTime;

    private bool OnBreak;

    private NavigateTo nav;

    private GameControl control;

    // Start is called before the first frame update
    void Start()
    {
        DiseaseCode = DiseaseType.Clean;
        OnBreak = true;
        PassedTime = 0;
        nav = GetComponent<NavigateTo>();
        control = GameObject.Find("Control").GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnBreak)
        {
            PassedTime += Time.deltaTime;
            if (PassedTime > ChosenTime)
            {
                ChosenTime = UnityEngine.Random.Range(BreakTimeMin, BreakTimeMax);
                OnBreak = true;
                PassedTime = 0;
                nav.GoToObject(control.BreakObjects[UnityEngine.Random.Range(0, control.BreakObjects.Count)], 6);
            }
        }
        else
        {
            PassedTime += Time.deltaTime;
            if (PassedTime > ChosenTime)
            {
                ChosenTime = UnityEngine.Random.Range(WorkTimeMin, WorkTimeMax);
                OnBreak = false;
                PassedTime = 0;
                nav.GoToObject(Office.transform.Find("StandingLocation").gameObject, 3);
            }
        }
    }

    //Returns true if this computer resists the disease
    public bool PassedResistanceCheck(DiseaseType Disease)
    {
        int Resistance = 0;
        switch(Disease)
        {
            case DiseaseType.Clean:
                break;
            case DiseaseType.DOS:
                break;
            case DiseaseType.Password:
                Resistance = ResistPassword;
                break;
            case DiseaseType.Phish:
                Resistance = ResistPhish;
                break;
            case DiseaseType.Upload:
                Resistance = ResistUpload;
                break;
        }
        if (UnityEngine.Random.Range(0, 100) > Resistance)
        {
            return false;
        }
        return true;
    }

    public void Infected(DiseaseType Disease, bool Mystery)
    {
        DiseaseCode = Disease;
        this.Mystery = Mystery;
    }
}
