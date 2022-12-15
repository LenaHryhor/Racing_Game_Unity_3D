using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Controls : MonoBehaviour
{
    public float speed = 0f; 
    public float maxSpeed = 0.4f; 
    public float sideSpeed = 0f;

    public float scores = 0f; 
    public float highScore = 0f;

    void Start()
    {
        string high = File.ReadAllText("highscore");

        try
        {
            highScore = Convert.ToSingle(high);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        float moveSide = Input.GetAxis("Horizontal"); 
        float moveForward = Input.GetAxis("Vertical");

        if (moveSide != 0)
        {
            sideSpeed = moveSide * -1f; 
        }

        if (moveForward != 0)
        {
            speed += 0.05f * moveForward; 
        }
        else 
        {
            if (speed > 0)
            {
                speed -= 0.05f;
            }
            else
            {
                speed += 0.05f;
            }
        }

        if (speed > maxSpeed)
        {
            speed = maxSpeed; 
        }

    }
}
