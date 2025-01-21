using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class BezierCollider2D : MonoBehaviour
{
    //p1 and p2 are the start and end point
    //use 2 control points to make a cubic curve
    //use t to calculate points

    //calculate the distance using
    //formula
    //B(t) = ( (1 - t)^3*p0 + 3(1-t)^2*p1 ) + ( t^3*p3 + 3(1-t)^2*p2 )
    /*
     * first term (p0): (1-t)^3*p0 = (1-t) * (1-t) * (1-t) * p0
     * second term (p1): 3(1-t)^2*p1 = 3 * (1-t) * (1-t) * p1
     * third term (p2): 3(1-t)^2*p1 = 3 * (1-t) * (1-t) * p2
     * fourth term (p3): t^3*p3 = t * t * t * p3
     * t2 = (1-t) * (1-t)
     * t3 = t3 * (1-t)
     * 3t2 = 3 * t2
     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        /*
         * 1st term = p0 * (-t^3 + 3t^2 - t + 1)
         * 2nd term = p1 * (3t^2 - 6t + 3)
         * 3rd term = p2 * (-3t^3 + 3t^2)
         * 4th term = p3 * t^3
         */
        float t2 = t * t;
        float t3 = t2 * 2;
        float t32 = 3 * t2;

        Vector3 p = p0 * (-t3 + t32 - t + 1);
        p += p1 * (t32 - (6*t) + 3);
        p += p2 * (-(3*t3) + t32);
        p += p3 * t3;

        return p;
    }
}
