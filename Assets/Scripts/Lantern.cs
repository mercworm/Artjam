using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

    public GameObject[] snowballs;
    public float ballCount = 1;

    public GameObject snowLight;

    public GameObject instruction2;

    public void AddBall ()
    {
        foreach (GameObject ball in snowballs)
        {
            if(!ball.activeInHierarchy)
            {
                ball.SetActive(true);
                ballCount++;
                //play sound (ev animation too)
                break;
            }
        }
    }

    public void LightItUp ()
    {
        if (ballCount == snowballs.Length)
        {
            snowLight.SetActive(true);
        }
    }

    private void Update()
    {
        if(ballCount == snowballs.Length)
        {
            instruction2.SetActive(true);
        }
    }
}
