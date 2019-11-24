using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeControl : MonoBehaviour
{
    public String Name;
    public float SpamProb;

    public GameObject Office;

    public float BreakChance;
    public float BreakTimeMin;
    public float BreakTimeMax;
    public float WorkTime;

    private float ChosenBreakTime;
    private float PassedTime;

    private bool OnBreak;

    private NavigateTo nav;

    private GameControl control;

    // Start is called before the first frame update
    void Start()
    {
        OnBreak = false;
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
            if (PassedTime > WorkTime)
            {
                if (UnityEngine.Random.Range(0f, 1f) < BreakChance)
                {
                    ChosenBreakTime = UnityEngine.Random.Range(BreakTimeMin, BreakTimeMax);
                    OnBreak = true;
                    PassedTime = 0;
                    nav.GoToObject(control.BreakObjects[UnityEngine.Random.Range(0, control.BreakObjects.Count)]);
                }
            }
        }
        else
        {
            PassedTime += Time.deltaTime;
            if (PassedTime > ChosenBreakTime)
            {
                OnBreak = false;
                PassedTime = 0;
                nav.GoToObject(Office.transform.Find("StandingLocation").gameObject);
            }
        }
    }
}
