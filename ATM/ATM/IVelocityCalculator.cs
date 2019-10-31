namespace ATM
{
    public interface IVelocityCalculator
    {
        double CalculateSpeed(TrackData prevData, TrackData currData);
    }
}