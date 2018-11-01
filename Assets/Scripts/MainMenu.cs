using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public KeyCode startKey, endKey;

    public GameObject fadeScreen;

    private void OnEnable()
    {
        EventManager.StartListening("ThroughExitDoor", BackToMenu);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(startKey))
        {
            //start the game!
            if (SceneManager.GetActiveScene().name == "MainMenu") StartCoroutine(SceneTransition("GameScene", 1.5f));
            else return;
        }
        if(Input.GetKeyDown(endKey))
        {
            //depending on which scene we're in, either quit the game or return to menu
            if (SceneManager.GetActiveScene().name == "MainMenu") EndGame();
            else return;
        }
	}

    public void EndGame ()
    {
        Application.Quit();
        Debug.Log("Game has quit.");
    }

    public void BackToMenu ()
    {
        StartCoroutine(SceneTransition("MainMenu", 3f));
    }

    public IEnumerator SceneTransition (string sceneName, float waitTime)
    {
        var fade = fadeScreen.GetComponent<Animator>();
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
        fade.SetTrigger("FadeIn");
    }
}
