using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour {

    public List<AudioClip> checkList;

    void Check ()
    {
        foreach(AudioClip clip in badList)
        {
            if (clip == source.clip)
            {
                foreach(AudioClip checkClip in checkList)
                {
                    if(checkClip == clip)
                    {
                        //do double
                    }
                    else
                    {
                        checkList.Add(clip);
                    }
                }
            }
        }
    }

    public List<AudioClip> badList = new List<AudioClip>();
    public List<AudioClip> goodList = new List<AudioClip>();
    public List<AudioClip> rareList = new List<AudioClip>();

    private AudioSource source;

    public int voiceCounts;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    //this is the function you should call that starts the hello
    public void StartVoiceLines()
    {

    }

    //this is the function you should call every time you want her to say something random.
    public void OnButtonPress()
    {
        var randomList = Random.Range(0, 100);
        if (randomList <= 3)
        {
            //play something from the rare list
            PlayFromList(rareList);
        }
        else if (randomList >= 4 || randomList <= 47)
        {
            //play something from good list
            PlayFromList(goodList);
        }
        else if (randomList >= 48 || randomList <= 100)
        {
            //play something from bad list
            PlayFromList(badList);
        }
    }

    //plays something from either or the lists
    //depends on what list you call with the function
    public void PlayFromList(List<AudioClip> list)
    {
        var randomTrack = Random.Range(0, list.Count);
        source.clip = list[randomTrack];
        source.Play();

        voiceCounts++;
    }

    //something special happens every 10th voiceline
    private void Update()
    {
        if(voiceCounts == 10)
        {

        }
        else if (voiceCounts == 20)
        {

        }
        else if (voiceCounts == 30)
        {

        }
    }
}
