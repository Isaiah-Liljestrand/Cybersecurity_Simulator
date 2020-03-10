using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public KeyCode pan;

    public float minsize;
    public float maxsize;

    public float zoomspeed;

    private Vector3 lastpos;

    public List<Camera> zoomcameras;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pan))
        {
            lastpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetKey(pan))
        {
            //Gets the delta of the worldPos and mousePos
            Vector3 worldPosDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastpos;

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - worldPosDelta.x, Camera.main.transform.position.y - worldPosDelta.y, Camera.main.transform.position.z - worldPosDelta.z);

            //Set previous variables to current state for use in next frame
            lastpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomspeed;
        if (Camera.main.orthographicSize < minsize)
            Camera.main.orthographicSize = minsize;
        if (Camera.main.orthographicSize > maxsize)
            Camera.main.orthographicSize = maxsize;

        foreach (Camera cam in zoomcameras)
            cam.orthographicSize = Camera.main.orthographicSize;
    }
}
