using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

    public Transform wentBack, wentForward;
    public GameObject player, fadePanel;

    private Animator fadeAnim;
    private bool goingForwards;

    private FirstPersonController fpc;

    public float roomCount;
    public GameObject[] snowballs;

    public GameObject lighter, instruction1, cantGoBack, cantGoForward, pedestal;

    private bool first = true;

    private void Start()
    {
        fpc = GetComponent<FirstPersonController>();
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
    }

    public void ThroughExitDoor ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
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
        player.transform.position = whichWay.transform.position;

        if (instruction1.activeInHierarchy)
        {
            instruction1.SetActive(false);
        }
        RoomChange();

        //tried to make the character turn after movement
      //if (fpc.m_MouseLook.m_CameraTargetRot != null && fpc.m_MouseLook.m_CharacterTargetRot != null)
      //{
      //    fpc.m_MouseLook.m_CameraTargetRot = Quaternion.Euler(0f, +180f, 0f);
      //    fpc.m_MouseLook.m_CharacterTargetRot = Quaternion.Euler(0f, +180f, 0f);
      //}
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
        }
        else
        {
            if(lighter != null) lighter.SetActive(false);
            if (!pedestal.activeInHierarchy) pedestal.SetActive(true);
            cantGoBack.SetActive(false);
        }

        if (roomCount == 9)
        {
            cantGoForward.SetActive(true);
        }
        else
        {
            cantGoForward.SetActive(false);
        }
    }

    public void DisableWalls ()
    {
        cantGoBack.SetActive(true);
        cantGoForward.SetActive(true);
    }

    public void SnowDone ()
    {
        cantGoForward.SetActive(true);
        roomCount = 0;
    }
}
