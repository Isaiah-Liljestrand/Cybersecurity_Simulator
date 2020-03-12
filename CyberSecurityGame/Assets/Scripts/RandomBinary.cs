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
    private TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        while (text.text.Length <= length)
        {
            if (includespaces)
                text.text += " 10"[Random.Range(0, 2)];
            else
                text.text += "10"[Random.Range(0, 1)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (includespaces)
            text.text += " 10"[Random.Range(0, 2)];
        else
            text.text += "10"[Random.Range(0, 1)];

        if (text.text.Length > length)
            text.text = text.text.Substring(cutoff);
    }
}
