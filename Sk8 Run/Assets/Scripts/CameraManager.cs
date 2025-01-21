using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private GameObject WorldBounds;

    //[SerializeField]
    //private float followSpeed;

    [SerializeField]
    private Transform target;
     
    private float minX;

    private float minY;

    private float maxX;

    private float maxY;

    private Bounds bounds;

    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private Vector3 followSpeed = Vector3.zero;

    [SerializeField]
    private float smoothTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        InitializeProperties();
        
    }

    private void InitializeProperties()
    {
        bounds = WorldBounds.GetComponent<WorldBoundsHandler>().GetCameraBounds(2 * mainCamera.orthographicSize, 2 * mainCamera.orthographicSize * mainCamera.aspect);
        minX = bounds.min.x;
        minY = bounds.min.y;
        maxX = bounds.max.x;
        maxY = bounds.max.y;
        mainCamera.transform.position = new Vector3(minX, minY) + offset;
        //Debug.Log("minx: "+minX+" miny: " +minY +" maxx: "+ maxX + " maxY: "+maxY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("is in bounds: "+ isInBounds());
        Follow();
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private bool isInBounds()
    {
        float xpos = target.transform.position.x;
        float ypos = target.transform.position.y;
        if (xpos > minX && xpos < maxX && ypos > minY && ypos < maxY)
        {
            return true;
        }
        return false;
       
    }

    private void Follow()
    {
        float newX = target.position.x;
        float newY = target.position.y;

        if (newX < minX) newX = minX;
        else if (newX > maxX) newX = maxX;

        if (newY < minY) newY = minY;
        else if (newY > maxY) newY = maxY;

        Vector3 newPos = new Vector3(newX, newY) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref followSpeed, smoothTime);
        //transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);

    }

    //pan until player within bounds or camera hits bounds
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector3 P1 = new Vector2(minX, minY);
        Vector3 P2 = new Vector2(minX, maxY);
        Vector3 P3 = new Vector2(maxX, maxY);
        Vector3 P4 = new Vector2(maxX, minY);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(P1, P2);
        Gizmos.DrawLine(P2, P3);
        Gizmos.DrawLine(P3, P4);
        Gizmos.DrawLine(P4, P1);
    }
}
