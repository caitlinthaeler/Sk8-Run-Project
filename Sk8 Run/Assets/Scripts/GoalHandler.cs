using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    [SerializeField]
    private Color inactivatedColor;

    [SerializeField]
    private Color activatedColor;

    private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        InitializeGoal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeGoal()
    {
        UpdateGoalColor(inactivatedColor);
    }

    private void UpdateGoalColor(Color newColor)
    {
        currentColor = newColor;
        gameObject.GetComponent<SpriteRenderer>().color = newColor;
    }

    public void ActivateGoal()
    {
        UpdateGoalColor(activatedColor);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("collided with player");
            ActivateGoal();
        }
       
    }


    

}
