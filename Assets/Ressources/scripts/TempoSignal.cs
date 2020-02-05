using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoSignal : MonoBehaviour
{
    private float startingTime;
    private float currentTime;
    public float bpm = 120;
    public float tolerance = 0.2f;
    private AudioSource music;
    public AudioSource snare;
    public float audioOffset = 0.25f;
    private bool hasBeaten = false;
    private bool hasBeaten2 = false;
    private float n;
    private float somme;

    void Start()
    {
        music = GetComponent<AudioSource>();
        music.Play();
        startingTime = music.time + audioOffset;
        currentTime = startingTime;
        n = 0;
        somme = 0;
    }

    void Update()
    {
        
    }

    public float indicateBeatPlayer()
    {
        float beatDistance = distanceToBeat();

        if (beatDistance < tolerance)
        {
            //float tmp = distanceToBeat == toNextBeat ? -distanceToBeat : distanceToBeat;
            //somme += tmp;
            //n++;
            //print($"this one: {tmp} moy: {somme / n}");
            return (1);
        }
        if (beatDistance > tolerance)
            hasBeaten = false;
        //print(distanceToBeat);
        return (0);
    }
    public float indicateBeat()
    {
        float currentTime = music.time - startingTime;
        float spb = 60 / bpm;
        float toPreviousBeat = currentTime % spb;

        if (toPreviousBeat < spb / 2)
            return 1;
        else
            hasBeaten = false;
        return 0;
    }

    public float distanceToBeat()
    {
        float currentTime = music.time - startingTime;
        float spb = 60 / bpm;
        float toPreviousBeat = currentTime % spb;
        float toNextBeat = spb - toPreviousBeat;
        float distanceToBeat = toPreviousBeat < toNextBeat ? toPreviousBeat : toNextBeat;

        return (distanceToBeat);
    }


}
