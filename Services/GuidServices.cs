namespace ApiCoreID.Services
{
    public class GuidServices
    {
        public readonly Guid resultadoGuid;

        public GuidServices()
        {
            resultadoGuid = Guid.NewGuid();
        }
    }
}
