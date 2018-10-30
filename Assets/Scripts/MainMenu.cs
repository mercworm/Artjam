using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public KeyCode startKey, endKey;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(startKey))
        {
            //start the game!
            StartGame();
        }
        if(Input.GetKeyDown(endKey))
        {
            //depending on which scene we're in, either quit the game or return to menu
            if (SceneManager.GetActiveScene().name == "MainMenu") EndGame();
            else BackToMenu();
        }
	}

    //these are in separate voids so we can choose if we want to use ui buttons or keyboard presses when in menus
    public void StartGame ()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame ()
    {
        Application.Quit();
        Debug.Log("Game has quit.");
    }

    public void BackToMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
