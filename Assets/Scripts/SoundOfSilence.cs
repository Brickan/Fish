using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOfSilence : MonoBehaviour
{
    private AudioMixerSnapshot silence;

    [SerializeField]
    private float timeToChange;
    [SerializeField]
    private bool minutesFade;


    // Start is called before the first frame update
    void Start()
    {
        silence = GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.FindSnapshot("Silence");

        timeToChange = (timeToChange == 0) ? timeToChange = 2 : timeToChange;

        TimeMultiplierMath();
    }

    // Update is called once per frame
    void Update()
    {
        TowardsSilence();
    }

    void TowardsSilence()
    {
        silence.TransitionTo(timeToChange);
    }

    //this actually makes it the time it takes to make the transition so we don't have to make any fancy calculations to get how much we have to update something each second
    void TimeMultiplierMath()
    {
        if (minutesFade)
        {
            timeToChange = 60f * timeToChange;
        }

    }
}
