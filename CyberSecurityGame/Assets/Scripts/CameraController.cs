using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public KeyCode pan;

    public float minsize;
    public float maxsize;

    public Vector3 maxchange;

    public float zoomspeed;

    private Vector3 lastpos;
    private Vector3 firstpos;

    public List<Camera> zoomcameras;

    private void Start()
    {
        firstpos = transform.position;
    }

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

            float newx = Mathf.Clamp(Camera.main.transform.position.x - worldPosDelta.x, firstpos.x - maxchange.x, firstpos.x + maxchange.x);
            float newy = Mathf.Clamp(Camera.main.transform.position.y - worldPosDelta.y, firstpos.y - maxchange.y, firstpos.y + maxchange.y);
            float newz = Mathf.Clamp(Camera.main.transform.position.z - worldPosDelta.z, firstpos.z - maxchange.z, firstpos.z + maxchange.z);
            Camera.main.transform.position = new Vector3(newx, newy, newz);
            Debug.Log(Camera.main.transform.position);

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
