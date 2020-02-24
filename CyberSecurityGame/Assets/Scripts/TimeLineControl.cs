using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineControl : MonoBehaviour
{
    public List<Disease> diseases;
    public TimeLineUIScript timelineui;
    private int maxcount;

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
        List<TimeLineEventType> newevents = AddEvents(maxcount);
        foreach(TimeLineEventType tevent in newevents)
        {
            timelineui.AddEvent(tevent);
        }
    }

    public void DiseaseSolved(Disease disease)
    {

    }

    private List<TimeLineEventType> AddEvents(int lookahead)
    {
        List<TimeLineEventType> newevents = new List<TimeLineEventType>();
        newevents.Add(TimeLineEventType.nothing); //Add turns
        //Add disease spreads after turn passes.
        foreach (Disease disease in diseases)
        {
            if ((disease.currentturn + lookahead) % disease.turnsbeforespread == 0) //This dease will spread
                newevents.Add(TimeLineEventType.unknownvirus);
        }
        return newevents;
    }
}
