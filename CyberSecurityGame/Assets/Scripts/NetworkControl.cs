﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkControl : MonoBehaviour
{
    public Material LineMaterial;
    
    
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

    public void set_infection_status(GameObject node, DiseaseType status_code) {

        int i;
        int name_index;

        if (status_code == DiseaseType.Clean) { // cure it
            int.TryParse(node.gameObject.name.Substring(4),out name_index);
            
            
            for (i = 0;i <= 15; i++) {

                if (lines[name_index,i] != null) {

                    lines[name_index,i].startColor = Color.white;
                    lines[name_index, i].endColor = Color.white;


                }

            }
            node.transform.Find("NetworkNode").gameObject.GetComponent<Renderer>().material.color = Color.white;

        } else if (status_code == DiseaseType.Password){ // infection type 1

            int.TryParse(node.gameObject.name.Substring(4), out name_index);


            for (i = 0; i <= 15; i++)
            {

                if (lines[name_index, i] != null)
                {
                    if (lines[name_index, i].startColor != Color.cyan && lines[name_index, i].endColor != Color.cyan)
                     {

                        lines[name_index, i].startColor = Color.red;
                        lines[name_index, i].endColor = Color.red;
                    }

                }

            }

            node.transform.Find("NetworkNode").gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (status_code == DiseaseType.Phish){ // infection type 2

            int.TryParse(node.gameObject.name.Substring(4), out name_index);


            for (i = 0; i <= 15; i++)
            {

                if (lines[name_index, i] != null)
                {
                    if (lines[name_index, i].startColor != Color.cyan && lines[name_index, i].endColor != Color.cyan)
                    {

                        lines[name_index, i].startColor = Color.yellow;
                        lines[name_index, i].endColor = Color.yellow;
                    }

                }

            }
            node.transform.Find("NetworkNode").gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (status_code == DiseaseType.Upload){// infection type 3
            int.TryParse(node.gameObject.name.Substring(4), out name_index);
            

            for (i = 0; i <= 15; i++)
            {

                if (lines[name_index, i] != null)
                {
                    if (lines[name_index, i].startColor != Color.cyan && lines[name_index, i].endColor != Color.cyan)
                    {
                        lines[name_index, i].startColor = Color.green;
                        lines[name_index, i].endColor = Color.green;
                    }

                }

            }

            node.transform.Find("NetworkNode").gameObject.GetComponent<Renderer>().material.color = Color.green;




        }
        else if (status_code == DiseaseType.DOS)// DDOS
        {
            int.TryParse(node.gameObject.name.Substring(4), out name_index);


            for (i = 0; i <= 15; i++)
            {

                if (lines[name_index, i] != null)
                {

                    lines[name_index, i].startColor = Color.grey;
                    lines[name_index, i].endColor = Color.grey;


                }

            }

            node.transform.Find("NetworkNode").gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }

    }



}
