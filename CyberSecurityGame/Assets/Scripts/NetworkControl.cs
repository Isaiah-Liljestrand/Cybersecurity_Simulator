using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkControl : MonoBehaviour
{
    public Material LineMaterial;
    private int[,] Connections = new int[16, 16]
    {//   0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15
        { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //0
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //1
        { 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, //2
        { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //3
        { 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //4
        { 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1}, //5
        { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0}, //6
        { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0}, //7
        { 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0}, //8
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0}, //9
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0}, //10
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0}, //11
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0}, //12
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1}, //13
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1}, //14
        { 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0}  //15
    };
    private List<List<LineRenderer>> lines;
    private GameObject LineObject;

    public void ShowLines()
    {
        LineObject.SetActive(true);
    }

    public void HideLines()
    {
        LineObject.SetActive(false);
    }

    public void CreateLines(List<GameObject> DeskObjects)
    {
        List<Vector3> CoordinatesOfNodes = new List<Vector3>();
        foreach(GameObject obj in DeskObjects)
        {
            CoordinatesOfNodes.Add(obj.transform.Find("NetworkNode").transform.position);
        }

        LineObject = new GameObject();
        LineObject.layer = 8;
        int count = 0;
        lines = new List<List<LineRenderer>>();
        for (int i = 0; i < 16; i++)
        {
            List<LineRenderer> newlist = new List<LineRenderer>();
            for (int j = 0; j < 16; j++)
            {
                if (j >= i && Connections[i, j] == 1)
                {
                    GameObject ChildLine = new GameObject();
                    ChildLine.transform.SetParent(LineObject.transform);
                    ChildLine.layer = 8;
                    ChildLine.AddComponent<LineRenderer>();
                    LineRenderer newline = ChildLine.GetComponent<LineRenderer>();
                    newlist.Add(newline);
                    newline.startColor = Color.white;
                    newline.endColor = Color.white;
                    newline.startWidth = 0.5f;
                    newline.startWidth = 0.5f;
                    newline.material = LineMaterial;
                    newline.SetPositions(new Vector3[] {CoordinatesOfNodes[i], CoordinatesOfNodes[j]});
                    count++;
                }
            }
            lines.Add(newlist);
        }
        LineObject.SetActive(false);
    }

    public void set_infection_status(GameObject node, int status_code) {

        if (status_code < 1) {

            status_code = 1;

        }else if(status_code > 5) {

            status_code = 5;

        }else if (status_code == 1) { // cure it
            node.transform.Find("NetworkNode");

        } else if (status_code == 2){ // infection type 1
        
            node.transform.Find("NetworkNode");

        }else if (status_code == 3){ // infection type 2
        
            node.transform.Find("NetworkNode");

        }else if (status_code == 4){// infection type 3
        
            node.transform.Find("NetworkNode");

        }else if (status_code == 5)// DDOS
        {
            node.transform.Find("NetworkNode");

        }

    }



}
