using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomGlyph : MonoBehaviour
{
    private char minchar = '\ue6aa';
    private char maxchar = '\ue67f';
    private TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        RandomChar();
    }

    public void RandomChar()
    {
        text.text = "" + (char)(minchar + Random.Range(0, maxchar - minchar));
    }
}
