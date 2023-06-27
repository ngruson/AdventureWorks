using AW.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.REST.API;

public class Application : IApplication
{
    public string Namespace => typeof(Application).Namespace!;

    public string AppName => "ReferenceData.REST.API";
}
