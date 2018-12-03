namespace Interfaces
{
    public interface ITrackingNumberFormatter
    {
        /// <summary>
        /// Devuelve un string con el formato N:PostalCode:HashOrderId:
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        string CreateTrackingNumber(ITrackingInfo info);
    }
}
