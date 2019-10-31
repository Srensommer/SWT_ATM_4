namespace ATM
{
    public interface IDirectionCalculator
    {
        double CalculateDirection(TrackData prevData, TrackData currData);
        double RadiansToDegrees(double radians);
    }
}