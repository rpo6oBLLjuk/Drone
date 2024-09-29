using CustomInspector;
using UnityEngine;

public class AudioService : MonoBehaviour, IDroneInputUser
{
    [HorizontalLine("Short sounds data")]
    [SerializeField] private AudioSource _shortAudioSourse;
    [SerializeField] private ReorderableDictionary<string, AudioClip> _shortSounds;
    [SerializeField] private ShortAudioPlayer _shortAudioPlayer = new();

    [SerializeField] private AudioSource droneScrewAudioSourve;
    [SerializeField] private DroneScrewSoundPlayer droneScrewSoundPlayer = new();

    public DroneInput DroneInput { get; set; }

    public void PlayCheckpointCollected()
    {
        _shortAudioPlayer.PlaySound(_shortAudioSourse, _shortSounds["checkpoint"]);
    }

    public void PlayLevelComplete()
    {
        _shortAudioPlayer.PlaySound(_shortAudioSourse, _shortSounds["levelcomplete"]);
    }

    public void Update()
    {
        float upValue = DroneInput.Drone.Up.ReadValue<float>();
        float downValue = DroneInput.Drone.Down.ReadValue<float>();

        float speed = 0;
        bool isUp = true;

        if(upValue > 0)
        {
            isUp = true;
            speed = upValue;
        }
        else if(downValue > 0)
        {
            isUp = false;
            speed = downValue;
        }

        droneScrewSoundPlayer.Update(droneScrewAudioSourve, speed, isUp);
    }
}
