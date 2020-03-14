using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResearchWidgets
{
    public bool extraCleanAttempt;          //Gives player extra chances to clean computer without using up a turn
    public bool infectionInTimeline;        //Adds infection information to the timeline
    public bool eliminateWrongOptions;      //Removes the wrong options from being chooseable for compter cleaning
    public bool spreadView;                 //Allows for the player to see the spread of diseases for a certain ammount of time
    public bool improvedObservation;        //When investigating people, additional information becomes available....such as sticky note with password or usb drive
    public bool getACoolHat;                //For tutorial purposes?????

    void Start()
    {
        extraCleanAttempt = false;
        eliminateWrongOptions = false;
        spreadView = false;
        improvedObservation = false;
        getACoolHat = false;
    }

    public void InfectionInTimeline()
    {
        infectionInTimeline = true;
        //Change UI
    }
    public void ExtraCleanAttempt()
    {
        extraCleanAttempt = true;
        //Notify computer interface controller
    }

    public void EliminateWrongOptions()
    {
        eliminateWrongOptions = true;
        //Notify computer interface controller
    }

    public void SpreadView()
    {
        spreadView = true;
        //Notify networkview
    }

    public void ImprovedObservation()
    {
        improvedObservation = true;
        //Nofity dialogue control
    }

    public void GetACoolHat()
    {
        getACoolHat = true;
        //Get a dope ass hat
    }
}
