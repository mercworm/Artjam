using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballScript : MonoBehaviour {

    public void GotFound ()
    {
        //play sound
        //do particle effect
        Invoke("DestroyThis", 1f);
    }

    public void DestroyThis ()
    {
        Destroy(gameObject);
    }
}
