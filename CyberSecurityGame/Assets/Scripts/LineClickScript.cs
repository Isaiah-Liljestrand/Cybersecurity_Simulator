using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineClickScript : MonoBehaviour
{

    public GameObject parent_line;
    public int x;
    public int y;
    public NetworkControl net;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        parent_line = transform.parent.gameObject;

        if (parent_line.GetComponent<LineRenderer>().startColor == Color.cyan && parent_line.GetComponent<LineRenderer>().endColor == Color.cyan)
        {

            parent_line.GetComponent<LineRenderer>().startColor = Color.white;
            parent_line.GetComponent<LineRenderer>().endColor = Color.white;
            net.Connections[x, y] = 1;
            net.Connections[y, x] = 1;
        }
        else {


            parent_line.GetComponent<LineRenderer>().startColor = Color.cyan;
            parent_line.GetComponent<LineRenderer>().endColor = Color.cyan;
            net.Connections[x, y] = 2;
            net.Connections[y, x] = 2;

        }       


        
    }
}
