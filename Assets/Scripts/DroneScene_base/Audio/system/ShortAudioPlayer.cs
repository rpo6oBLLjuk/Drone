using UnityEngine;

public class ShortAudioPlayer 
{
    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
