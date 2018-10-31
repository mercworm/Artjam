using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lantern : MonoBehaviour {

    [System.Serializable] public class LightsOn : UnityEvent { }
    [SerializeField] public LightsOn lights, snowDone;

    public GameObject[] snowballs;
    public float ballCount = 1;

    public GameObject snowLight;

    public GameObject instruction2;

    private bool invokeCheck = true;

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
            lights.Invoke();
        }
    }

    private void Update()
    {
        if(ballCount == snowballs.Length)
        {
            if (invokeCheck)
            {
                invokeCheck = false;
                instruction2.SetActive(true);
                snowDone.Invoke();
            }
        }
    }
}
