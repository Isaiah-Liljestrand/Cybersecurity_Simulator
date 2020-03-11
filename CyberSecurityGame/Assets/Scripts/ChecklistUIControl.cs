using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChecklistUIControl : MonoBehaviour
{
    public GameObject panel;
    public GameObject checklist_unfilled;
    public GameObject checklist_filled;
    public float initialoffset;
    public float offset;

    private Canvas canvas;
    private List<GameObject> newpanels;
    private float currentoffset;

    private void Start()
    {
        currentoffset = initialoffset;
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (canvas.enabled && Input.GetMouseButton(0))
            TurnOffChecklist();
    }

    public void TurnOnChecklist()
    {
        canvas.enabled = true;
        newpanels = new List<GameObject>();
        currentoffset = initialoffset;
        //Get the checklist items from somewhere
        AddPanel(false, "Test1 That says some things");
        AddPanel(true, "Do the funky goose dance");
    }

    public void TurnOffChecklist()
    {
        foreach(GameObject p in newpanels)
        {
            Destroy(p);
        }
        canvas.enabled = false;
    }

    private void AddPanel(bool check, string text)
    {
        GameObject newpanel = Instantiate(check ? checklist_filled : checklist_unfilled);
        newpanel.transform.SetParent(panel.transform, false);
        newpanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, currentoffset);
        newpanel.GetComponentInChildren<TextMeshProUGUI>().text = text;
        currentoffset -= offset;
    }
}
