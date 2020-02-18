using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineControl : MonoBehaviour
{
    //public List<Disease> diseases;
    public TimeLineUIScript timelineui;
    private int maxcount;

    // Start is called before the first frame update
    void Start()
    {
        maxcount = timelineui.maxcount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
