using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeControl : MonoBehaviour
{
    public String Name;
    public int Index;
    public bool CanInvestigate;
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

    private bool Paused;

    private GameObject ExclamationMark;

    private float PassedCRTime;

    public GameObject speechbubble;
    public float speechbubblechance;
    public float speechbubblelifespan;
    private float speechbubbletime;

    private PlaySound playsound;

    // Start is called before the first frame update
    void Start()
    {
        OnBreak = true;
        nav = GetComponent<NavigateTo>();
        control = GameObject.Find("Control").GetComponent<GameControl>();
        playsound = GetComponent<PlaySound>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
        {
            if (speechbubble.activeSelf)
            {
                speechbubbletime += Time.deltaTime;
                if (speechbubbletime > speechbubblelifespan)
                {
                    speechbubble.SetActive(false);
                }
            }
            else
            {
                if (UnityEngine.Random.Range(0f, 1f) < speechbubblechance && OnBreak && !CanInvestigate)
                {
                    speechbubbletime = 0;
                    speechbubble.SetActive(true);
                    speechbubble.GetComponentInChildren<RandomGlyph>().RandomChar();
                    if (playsound)
                        playsound.PlayRandom();
                }
            }
            if (!OnBreak && !CanInvestigate)
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
    }

    public void Pause()
    {
        Paused = true;
        nav.Agent.isStopped = true;
    }

    public void Resume()
    {
        Paused = false;
        nav.Agent.isStopped = false;
    }

    public void SolveIssue()
    {
        CanInvestigate = false;
        Destroy(ExclamationMark);
        ExclamationMark = null;
    }
}
