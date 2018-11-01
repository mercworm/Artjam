using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActions : MonoBehaviour {

    [System.Serializable]
    public class DoorEvents : UnityEvent { }
    [SerializeField]
    public DoorEvents GoForwards, GoBack;

    public float distanceToSee;
    public LayerMask interactionsLayer;
    RaycastHit hit;

    public float invSnow = 0;

    private bool hasLighter = false;
    private bool hasMatches = false;
    private bool onlyOnce = false;
    public bool firstBall = true;

    public AudioSource source;
    public AudioClip snow, snow2, lightItUp, lighter, door1, door2, door3, lightItUp2, matches;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distanceToSee, Color.cyan);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distanceToSee))
        {
            Debug.Log("I touched " + hit.collider.gameObject.name);
        }

        //on click
        if (Input.GetKeyDown(KeyCode.E))
        {
            //draw ray
            RaycastHit[] rayHits;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            rayHits = Physics.RaycastAll(ray, distanceToSee, interactionsLayer);

            //checking every hit, so see if they match the tags
            for (int i = 0; i < rayHits.Length; i++)
            {
                Debug.Log("With the new Raycast, I touched " + rayHits[i].collider.gameObject.name);
                if (rayHits[i].collider.gameObject.tag == "ForwardDoor")
                {
                    GoForwards.Invoke();
                    source.PlayOneShot(door1);
                }
                else if (rayHits[i].collider.gameObject.tag == "BackDoor")
                {
                    GoBack.Invoke();
                    source.PlayOneShot(door2);
                }
                else if (rayHits[i].collider.gameObject.tag == "ExitDoor")
                {
                    source.PlayOneShot(door3);
                    EventManager.TriggerEvent("ThroughExitDoor");
                }
                else if (rayHits[i].collider.gameObject.tag == "Snowball")
                {
                    if (!onlyOnce)
                    {
                        onlyOnce = true;

                        var snowball = rayHits[i].collider.gameObject.GetComponent<SnowballScript>();
                        if (snowball != null)
                        {
                            snowball.GotFound();
                            source.PlayOneShot(snow);
                            invSnow++;
                            Debug.Log("Found snowball");
                        }

                        Invoke("PickUpBall", 1f);
                    }
                }
                else if (rayHits[i].collider.gameObject.tag == "Lantern")
                {
                    var lantern = rayHits[i].collider.gameObject.GetComponent<Lantern>();
                    if (lantern != null)
                    {
                        if (invSnow <= 0)
                        {
                            if(hasLighter || hasMatches)
                            {
                                lantern.LightItUp();
                                if (hasLighter) source.PlayOneShot(lightItUp);
                                else if (hasMatches) source.PlayOneShot(lightItUp2);
                            }
                            else return;
                        }
                        else
                        {
                            if (firstBall) firstBall = false;
                            lantern.AddBall();
                            invSnow--;
                            source.PlayOneShot(snow2);
                        }
                    }
                }
                else if (rayHits[i].collider.gameObject.tag == "Lighter")
                {
                    hasLighter = true;
                    Destroy(rayHits[i].collider.gameObject);
                    source.PlayOneShot(lighter);
                }
                else if(rayHits[i].collider.gameObject.tag == "Matches")
                {
                    hasLighter = true;
                    Destroy(rayHits[i].collider.gameObject);
                    source.PlayOneShot(matches);
                }
            }
        }
    }

    public void PickUpBall ()
    {
        onlyOnce = false;
    }
}
