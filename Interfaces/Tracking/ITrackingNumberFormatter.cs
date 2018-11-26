namespace Interfaces
{
    public interface ITrackingNumberFormatter
    {
        string CreateTrackingNumber(ITrackingInfo info);
    }
}
