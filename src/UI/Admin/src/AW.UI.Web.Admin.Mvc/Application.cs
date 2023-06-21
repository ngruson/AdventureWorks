using AW.SharedKernel.Interfaces;

namespace AW.UI.Web.Admin.Mvc;

public class Application : IApplication
{
    public string Namespace => typeof(Application).Namespace!;

    public string AppName => "Web.Admin.Mvc";
}
