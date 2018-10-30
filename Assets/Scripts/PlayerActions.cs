using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActions : MonoBehaviour {

    [System.Serializable]
    public class DoorEvents : UnityEvent { }
    [SerializeField]
    public DoorEvents GoForwards, GoBack, ExitGame;

    public float distanceToSee;
    public LayerMask interactionsLayer;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distanceToSee, Color.cyan);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distanceToSee))
        {
            Debug.Log("I touched " + hit.collider.gameObject.name);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit[] rayHits;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            rayHits = Physics.RaycastAll(ray, 200.0f, interactionsLayer);

            for (int i = 0; i < rayHits.Length; i++)
            {
                Debug.Log("With the new Raycast, I touched " + rayHits[i].collider.gameObject.name);
                if (rayHits[i].collider.gameObject.tag == "ForwardDoor")
                {
                    GoForwards.Invoke();
                }
                else if (rayHits[i].collider.gameObject.tag == "BackDoor")
                {
                    GoBack.Invoke();
                }
                else if (rayHits[i].collider.gameObject.tag == "ExitDoor")
                {
                    ExitGame.Invoke();
                }
            }
        }
    }
}
