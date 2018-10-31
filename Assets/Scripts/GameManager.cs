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

    private bool cantGoBack;
    private bool cantGoForward;

    public GameObject lighter, instruction1;

    private void Start()
    {
        fpc = GetComponent<FirstPersonController>();
        fadeAnim = fadePanel.GetComponent<Animator>();
    }

    public void ThroughExitDoor ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ThroughForwardDoor ()
    {
        if (cantGoForward)
        {
            var pl = player.GetComponent<PlayerActions>();
            pl.DoorIsLocked();
        }
        else
        {
            StartCoroutine(WhileMoving(wentForward));
            roomCount++;
        }
    }

    public void ThroughBackDoor ()
    {
        if(cantGoBack)
        {
            var pl = player.GetComponent<PlayerActions>();
            pl.DoorIsLocked();
        }
        {
            StartCoroutine(WhileMoving(wentBack));
            roomCount--;
        }
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
            lighter.SetActive(true);
            cantGoBack = true;
        }
        else
        {
            lighter.SetActive(false);
            cantGoBack = false;
        }

        if (roomCount == 9)
        {
            cantGoForward = true;
        }
        else
        {
            cantGoForward = false;
        }
    }
}
