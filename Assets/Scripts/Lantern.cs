using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lantern : MonoBehaviour {

    [System.Serializable] public class LightsOn : UnityEvent { }
    [SerializeField] public LightsOn lights, snowDone, threePickedUp;

    public GameObject[] snowballs;
    public float ballCount = 2;

    public GameObject snowLight, instruction2, thirdBall;

    private bool invokeCheck = true;
    private bool invokeCheck2 = true;

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
            Debug.Log("Ballcount adds up");
            if (invokeCheck)
            {
                invokeCheck = false;
                instruction2.SetActive(true);
                snowDone.Invoke();
            }
        }

        if(thirdBall == null  && invokeCheck2)
        {
            threePickedUp.Invoke();
        }
    }
}
