using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

    public Transform wentBack, wentForward;
    public GameObject player, playerCamera, fadePanel;

    private Animator fadeAnim;
    private bool goingForwards;

    public float roomCount;
    public GameObject[] snowballs;

    public GameObject lighter, instruction1, cantGoBack, cantGoForward, pedestal, exitDoor, missedAnything, goBack, matchBox, anotherWay, anotherWay2, ladder, exitText, rightWay, goalPainting;

    private bool first = true;
    private bool snowDone = false;
    private bool hasBeenInNine = false;

    public GameObject ExitLight;

    private void Start()
    {
        fadeAnim = fadePanel.GetComponent<Animator>();
    }

    private void Update()
    {
        if (first)
        {
            if (!player.GetComponent<PlayerActions>().firstBall)
            {
                cantGoForward.SetActive(false);
                first = false;
            }
        }

        if(lighter == null)
        {
            matchBox.SetActive(false);
            anotherWay.SetActive(false);
        }

        if(matchBox == null)
        {
            lighter.SetActive(false);
            anotherWay2.SetActive(true);
        }

        Lights();
    }

    public void ThroughForwardDoor ()
    {
        StartCoroutine(WhileMoving(wentForward));
        roomCount++;
    }

    public void ThroughBackDoor ()
    {
        StartCoroutine(WhileMoving(wentBack));
        roomCount--;
    }

    public IEnumerator WhileMoving (Transform whichWay)
    {
        fadeAnim.SetTrigger("Fade");
        yield return new WaitForSeconds(.4f);
        player.transform.localPosition = whichWay.transform.position;
        player.GetComponent<FirstPersonController>().rotationToggle = true;

        if (instruction1.activeInHierarchy)
        {
            instruction1.SetActive(false);
        }
        RoomChange();
    }

    public void RoomChange ()
    {
        foreach (GameObject ball in snowballs)
        {
            if (ball != null)
            {
                if (ball.name.Contains("Find_" + roomCount))
                {
                    ball.SetActive(true);
                }
                else
                {
                    ball.SetActive(false);
                }
            }
        }

        if(roomCount == -1)
        {
            if (lighter != null) lighter.SetActive(true);
            pedestal.SetActive(false);
            cantGoBack.SetActive(true);
            if(matchBox != null) matchBox.SetActive(true);
            anotherWay.SetActive(true);
        }
        else
        {
            if(lighter != null) lighter.SetActive(false);
            if (!pedestal.activeInHierarchy) pedestal.SetActive(true);
            cantGoBack.SetActive(false);
            if(matchBox != null) matchBox.SetActive(false);
            anotherWay.SetActive(false);
        }

        if (roomCount == 1)
        {
            goalPainting.SetActive(true);
        }
        else goalPainting.SetActive(false);

        if (roomCount == 9)
        {
            hasBeenInNine = true;
            cantGoForward.SetActive(true);
            goBack.SetActive(true);
        }
        else
        {
            cantGoForward.SetActive(false);
            goBack.SetActive(false);
        }

        if(roomCount == 0)
        {
            if (snowDone)
            {
                cantGoBack.SetActive(false);
                anotherWay2.SetActive(false);
            }
            else cantGoBack.SetActive(true);
        }

        if (roomCount == 4) exitText.SetActive(true);
        else exitText.SetActive(false);

        if (roomCount == 2) ladder.SetActive(true);
        else ladder.SetActive(false);

        if (roomCount == 3)
        {
            var ball3 = GameObject.Find("Snowball_Find_3");
            if (ball3 != null)
            {
                if (ball3.activeInHierarchy)
                {
                    missedAnything.SetActive(true);
                }
                else
                {
                    missedAnything.SetActive(false);
                }
            }
            else Debug.Log("Ball3 is gone");
        }
        else missedAnything.SetActive(false);

        if (roomCount == 8 && hasBeenInNine)
        {
            rightWay.SetActive(true);
        }
        else rightWay.SetActive(false);
    }

    public void DisableWalls ()
    {
        cantGoBack.SetActive(true);
        cantGoForward.SetActive(true);
        exitDoor.SetActive(true);
    }

    public void SnowDone ()
    {
        cantGoForward.SetActive(true);
        cantGoBack.SetActive(false);
        roomCount = 0;
        snowDone = true;
        Debug.Log("SnowDone has triggered");
    }

    //takes care of the exitlight turning on and off depending on the snowballs being found
    public void Lights ()
    {
        var currentSnowball = GameObject.FindGameObjectWithTag("Snowball");
        if (currentSnowball != null)
        {
            Debug.Log(currentSnowball);
            ExitLight.SetActive(false);
        }
        else ExitLight.SetActive(true);
    }
}
