using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public KeyCode startKey, endKey;

    public GameObject fadeScreen;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(startKey))
        {
            //start the game!
            StartCoroutine(SceneTransition("GameScene"));
        }
        if(Input.GetKeyDown(endKey))
        {
            //depending on which scene we're in, either quit the game or return to menu
            if (SceneManager.GetActiveScene().name == "MainMenu") EndGame();
            else StartCoroutine(SceneTransition("MainMenu"));
        }
	}

    public void EndGame ()
    {
        Application.Quit();
        Debug.Log("Game has quit.");
    }

    public IEnumerator SceneTransition (string sceneName)
    {
        var fade = fadeScreen.GetComponent<Animator>();
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        fade.SetTrigger("FadeIn");
    }
}
