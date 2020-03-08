using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkControl : MonoBehaviour
{
    public Material LineMaterial;
    public GameObject NetworkShadow;



    public int[,] Connections = new int[16, 16]
    {//   0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15
        { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //0
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //1
        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, //2
        { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //3
        { 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //4
        { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1}, //5
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
    private LineRenderer[,] lines = new LineRenderer[16, 16];
    //private List<List<LineRenderer>> lines;
    private GameObject LineObject;

    public int[] GetComputersToInfect(int[] infectedcomputers)
    {
        List<int> computers = new List<int>();
        foreach(int infectedcomputer in infectedcomputers) {
            for(int i = 0; i < 16; i++) {
                if(Connections[infectedcomputer][i] == 1) {
                    computers.Add(i);
                }
            }
        }
        return computers.ToArray();
    }

    public void ShowLines()
    {
        LineObject.SetActive(true);
    }

    public void HideLines()
    {
        LineObject.SetActive(false);
        set_all_nodes();
    }

    public void ShowNetwork()
    {
        NetworkShadow.SetActive(true);
        set_all_nodes();
        ShowLines();
        foreach (EmployeeDeskPair obj in GetComponent<GameControl>().employees)
        {
            obj.desk.transform.Find("NetworkNode").gameObject.SetActive(true);
        }
    }

    public void HideNetwork()
    {
        NetworkShadow.SetActive(false);
        HideLines();
        foreach (EmployeeDeskPair obj in GetComponent<GameControl>().employees)
        {
            obj.desk.transform.Find("NetworkNode").gameObject.SetActive(true);
        }
    }

    public void CreateLines(List<GameObject> DeskObjects)
    {
        List<Vector3> CoordinatesOfNodes = new List<Vector3>();
        foreach (GameObject obj in DeskObjects)
        {
            CoordinatesOfNodes.Add(obj.transform.Find("NetworkNode").transform.position);
        }
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                lines[i, j] = null;
            }
        }

        LineObject = new GameObject();
        LineObject.layer = 8;
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (j >= i && Connections[i, j] == 1)
                {
                    GameObject ChildLine = new GameObject();
                    GameObject box_trigger = new GameObject();

                    ChildLine.transform.SetParent(LineObject.transform);
                    ChildLine.layer = 8;
                    ChildLine.AddComponent<LineRenderer>();

                    box_trigger.transform.SetParent(ChildLine.transform);
                    box_trigger.AddComponent<BoxCollider>();
                    box_trigger.GetComponent<BoxCollider>().isTrigger = true;
                    box_trigger.AddComponent<LineClickScript>();

                    box_trigger.GetComponent<LineClickScript>().x = i;
                    box_trigger.GetComponent<LineClickScript>().y = j;
                    box_trigger.GetComponent<LineClickScript>().net = this;

                    
                    LineRenderer newline = ChildLine.GetComponent<LineRenderer>();
                    lines[i, j] = newline;
                    newline.startColor = Color.white;
                    newline.endColor = Color.white;
                    newline.startWidth = 0.5f;
                    newline.startWidth = 0.5f;
                    newline.material = LineMaterial;
                    newline.SetPositions(new Vector3[] { CoordinatesOfNodes[i], CoordinatesOfNodes[j] });
                    
                    float newline_length = Vector3.Distance(CoordinatesOfNodes[i], CoordinatesOfNodes[j]);
                    box_trigger.transform.position = CoordinatesOfNodes[i];
                    box_trigger.transform.LookAt(CoordinatesOfNodes[j]);
                    box_trigger.GetComponent<BoxCollider>().size = new Vector3(2.5f,2.5f, newline_length);
                    box_trigger.transform.position = (CoordinatesOfNodes[i] + CoordinatesOfNodes[j])/2;

                }
            }
        }
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (j < i)
                {
                    lines[i, j] = lines[j, i];
                }
            }
        }
        LineObject.SetActive(false);
    }


    public void set_all_nodes() {
        
        foreach (Disease disease_type in GetComponent<DiseaseControl>().diseases) {

            foreach (int node in disease_type.GetInfected()) {
                
                set_node_color(node, disease_type.color);
            }
        }
    }

    public void set_node_color(int node, Color color) {
            for (int i = 0;i <= 15; i++) {

                if (lines[node,i] != null) {

                    lines[node,i].startColor = color;
                    lines[node, i].endColor = color;
                }

            }
            GetComponent<GameControl>().employees[node].desk.transform.Find("NetworkNode").gameObject.GetComponent<Renderer>().material.color = color;
    }
}
