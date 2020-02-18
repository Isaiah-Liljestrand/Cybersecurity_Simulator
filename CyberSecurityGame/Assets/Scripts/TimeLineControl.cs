using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineControl : MonoBehaviour
{
    public List<Disease> diseases;
    public TimeLineUIScript timelineui;
    private int maxcount;

    private List<TimeLineEventType> upcomingevents;

    // Start is called before the first frame update
    void Start()
    {
        maxcount = timelineui.maxcount;
    }

    public void Initialpopulate()
    {
        for (int i = 0; i < maxcount; i++)
        {
            AddEvents(i);
        }
    }

    public void Turnpassed()
    {
        AddEvents(maxcount);
        timelineui.AddEvent
    }

    public void DiseaseSolved(Disease disease)
    {

    }

    private void AddEvents(int lookahead)
    {
        upcomingevents.Add(TimeLineEventType.nothing); //Add turns
        //Add disease spreads after turn passes.
        foreach (Disease disease in diseases)
        {
            if ((disease.currentturn + lookahead) % disease.turnsbeforespread == 0) //This dease will spread
                upcomingevents.Add(TimeLineEventType.unknownvirus);
        }
    }
}
