using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerOfTheRhythm : MonoBehaviour
{
    public TempoSignal globalTempo;
    private bool hasBeaten;
    private float bpm;

    void Start()
    {
        hasBeaten = false;
        bpm = globalTempo.bpm;
    }

    void Update()
    {
        if (globalTempo.indicateBeat() < 1 && hasBeaten)
        {
            hasBeaten = false;
        }
    }

    public bool canMoveToRythm()
    {
        //This is to be used by non human characters or decor.
        if (globalTempo.indicateBeat() > 0 && !hasBeaten)
        {
            hasBeaten = true;
            return (true);
        }
        return (false);
    }

    public bool canMoveToRhythmPlayer()
    {
        //This function works with a tolerance allowing hits from before to after the beat to be valid,
        //Allowing more human-like players to not be exact and still believe they are the shit
        if (globalTempo.indicateBeatPlayer() > 0 && !hasBeaten)
        {
            hasBeaten = true;
            return (true);
        }
        return (false);
    }
    
    public float getBpm()
    {
        return (bpm);
    }

    public float distanceToBeatSingleUse()
    {
        if (globalTempo.indicateBeat() > 0 && !hasBeaten)
        {
            hasBeaten = true;
            return (globalTempo.distanceToBeat());
        }
        return (0);
    }

}
