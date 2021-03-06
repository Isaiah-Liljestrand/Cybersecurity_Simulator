using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeLineEventType
{
    nothing,
    unknownvirus,
}

public class TimeLineUIScript : MonoBehaviour
{
    public int maxcount;
    public int previousturns;
    public float slidespeed;
    public float offset;
    public GameObject objectline;
    public GameObject circleprefab;
    public GameObject xprefab;

    private List<GameObject> spawned;
    private List<GameObject> tospawn;
    private float position;

    // Start is called before the first frame update
    void Start()
    {
        position = 0;
        spawned = new List<GameObject>();
        tospawn = new List<GameObject>();
    }

    private void AddObject(GameObject objtoadd)
    {
        tospawn.Add(objtoadd);
    }

    public void AddEvent(TimeLineEventType type)
    {
        switch(type)
        {
            case TimeLineEventType.nothing:
                AddObject(circleprefab);
                break;
            case TimeLineEventType.unknownvirus:
                AddObject(xprefab);
                break;
        }
    }

    public void RemoveEvent(int turnsahead)
    {
        if (turnsahead + previousturns < spawned.Count)
        {
            GameObject toremove = spawned[turnsahead + previousturns];
            spawned.RemoveAt(turnsahead + previousturns);
            for (int i = 0; i < turnsahead + previousturns; i++)
            {
                if (i < spawned.Count)
                    spawned[i].GetComponent<FillTimeLineEventUI>().moveto = spawned[i].transform.localPosition + (Vector3.right * offset);
            }
            toremove.GetComponent<FillTimeLineEventUI>().Fade();
            //IncrementEvents();
        }
    }

    public void IncrementEvents()
    {
        position -= offset;
        if (spawned.Count >= maxcount)
        {
            if (spawned.Count >= maxcount + previousturns)
            {
                spawned[0].GetComponent<FillTimeLineEventUI>().Fade();
                spawned.RemoveAt(0);
            }
            for (int i = 0; i < (spawned.Count - maxcount) + 1; i++)
            {
                if (i < spawned.Count)
                    spawned[i].GetComponent<FillTimeLineEventUI>().Fill();
            }
        }

        GameObject newobj = Instantiate(tospawn[0]);
        tospawn.RemoveAt(0);
        newobj.transform.SetParent(objectline.transform, false);
        newobj.transform.localPosition = new Vector3(-position + ((maxcount / 2) * offset), 0, 0);
        newobj.GetComponent<FillTimeLineEventUI>().moveto = newobj.transform.localPosition;
        newobj.GetComponent<FillTimeLineEventUI>().movespeed = slidespeed;
        spawned.Add(newobj);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddEvent(TimeLineEventType.nothing);
            IncrementEvents();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddEvent(TimeLineEventType.unknownvirus);
            IncrementEvents();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RemoveEvent(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RemoveEvent(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RemoveEvent(3);
        }

        if (spawned.Count > 0)
        {
            if (!spawned[0]) //Remove destroyed objects.
                spawned.RemoveAt(0);
        }

        //Slide the maintimeline object towards it's new position.
        Vector3 calcpos = objectline.transform.localPosition;
        calcpos.x = position;
        objectline.transform.localPosition = Vector3.MoveTowards(objectline.transform.localPosition, calcpos, slidespeed * Time.deltaTime);
    }
}
