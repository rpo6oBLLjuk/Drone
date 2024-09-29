using System;
using UnityEngine;

[Serializable]
public class DroneScrewSoundPlayer
{
    [SerializeField] private float maxVolume;

    public void Update(AudioSource source, float inputValue, bool isUp)
    {
        if (inputValue > 0)
        {
            if (!source.isPlaying)
                source.Play();

            source.volume = inputValue * maxVolume;

            //if (isUp)
            //    source.pitch = 2;
            //else
            //    source.pitch = -2;
        }
        else
            if (source.isPlaying)
            source.Stop();

    }
}
