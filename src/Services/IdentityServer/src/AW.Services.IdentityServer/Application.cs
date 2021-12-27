using AW.SharedKernel.Interfaces;

namespace AW.Services.IdentityServer
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace;

        public string AppName => "IdentityServer";
    }
}