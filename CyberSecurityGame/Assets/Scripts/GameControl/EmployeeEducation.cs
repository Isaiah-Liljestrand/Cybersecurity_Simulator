using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EmployeeEducation
{
    public bool canCureThemselves;          //10% chance to cure themselves each turn todo
    public bool canResistAttack;            //50% chance to repel disease that is targeting them
    public bool informationBoost;           //Gives second peice of information if available
    public bool slowsDiseaseSpread;         //Adds time to next spread time if this employee is infected todo

    public DistributeType immunity;

    void Start()
    {
        canCureThemselves = false;
        canResistAttack = false;
        informationBoost = false;
        slowsDiseaseSpread = false;
        immunity = DistributeType.None;
    }
}

