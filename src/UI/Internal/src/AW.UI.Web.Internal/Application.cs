using AW.SharedKernel.Interfaces;

namespace AW.UI.Web.Internal
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace;

        public string AppName => "Web.Internal";
    }
}