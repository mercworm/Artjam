using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballScript : MonoBehaviour {

    public void GotFound ()
    {
        //do particle effect
        Invoke("DestroyThis", .3f);
    }

    public void DestroyThis ()
    {
        Destroy(gameObject);
    }
}
