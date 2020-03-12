using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class RandomBinary : MonoBehaviour
{
    public int length;
    public bool includespaces;
    public int cutoff;
    public float time;
    private float timepassed;
    private TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        timepassed = Random.Range(0, time);
    }

    // Update is called once per frame
    void Update()
    {
        timepassed += Time.deltaTime;
        if (timepassed > time)
        {
            timepassed = 0;
            string newtext = "";
            for (int i = 0; i < length; i++)
            {
                if (includespaces)
                    newtext += " 10"[Random.Range(0, 2)];
                else
                    newtext += "10"[Random.Range(0, 1)];
            }
            text.text = newtext;
        }
    }
}
