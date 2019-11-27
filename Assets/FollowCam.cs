using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // The static point of interest.
    static public GameObject POI;

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
        // Force destination.z to be camZ to keep the camera far enough away.
        destination.z = camZ;
        // Set the camera to the destination.
        transform.position = destination;
    }
}
