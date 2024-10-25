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
        float throttleValue = DroneInput.Drone.Throttle.ReadValue<float>();

        float speed = throttleValue;
        bool isUp = throttleValue > 0;

        droneScrewSoundPlayer.Update(droneScrewAudioSourve, speed, isUp);
    }
}
