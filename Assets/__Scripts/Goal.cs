using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // A static field accessible by code anywhere.
    static public bool goalMet = false;

    void OnTriggerEnter(Collider other)
    {
        // When the trigger's hit by something check to see if its a projectile.
        if (other.gameObject.tag == "Projectile")
        {
            // If so, set goalMet to true.
            Goal.goalMet = true;
            // Also set the alpha of the color to higher opacity.
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
