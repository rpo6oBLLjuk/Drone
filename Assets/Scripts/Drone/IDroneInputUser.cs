public interface IDroneInputUser
{
    public DroneInput DroneInput { get; set; }

    public void SetDroneInput(DroneInput droneInput)
    {
        DroneInput = droneInput;
    }
}
