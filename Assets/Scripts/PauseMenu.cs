using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pausemenu;
    public bool pausemenuActive = false;
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausemenuActive)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
            else if (!pausemenuActive)
            {
                pausemenu.SetActive(true);
                pausemenuActive = true;
            }
        }

        if(pausemenuActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pausemenuActive = false;
                pausemenu.SetActive(false);
            }
        }
	}
}
