using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // The static point of interest.
    static public GameObject POI;

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    // The desired Z pos of the camera.
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        // Return if there's no POI.
        if (POI == null) return;

        // Get the position of the POI.
        Vector3 destination = POI.transform.position;
        // Limit the X & Y to minimum values.
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        // Interpolate from the current Camera position toward destination.
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Force destination.z to be camZ to keep the camera far enough away.
        destination.z = camZ;
        // Set the camera to the destination.
        transform.position = destination;
        // Set the orthographicSize of the Camera to keep the Ground in view.
        Camera.main.orthographicSize = destination.y + 10;
    }
}
