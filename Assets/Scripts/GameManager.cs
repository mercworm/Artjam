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
        StartCoroutine(WhileMoving(wentForward));
    }

    public void ThroughBackDoor ()
    {
        StartCoroutine(WhileMoving(wentBack));
    }

    public IEnumerator WhileMoving (Transform whichWay)
    {
        fadeAnim.SetTrigger("Fade");
        yield return new WaitForSeconds(.4f);
        player.transform.position = whichWay.transform.position;

        //tried to make the character turn after movement
      //if (fpc.m_MouseLook.m_CameraTargetRot != null && fpc.m_MouseLook.m_CharacterTargetRot != null)
      //{
      //    fpc.m_MouseLook.m_CameraTargetRot = Quaternion.Euler(0f, +180f, 0f);
      //    fpc.m_MouseLook.m_CharacterTargetRot = Quaternion.Euler(0f, +180f, 0f);
      //}
    }
}
