namespace AW.UI.Web.Store.ViewModels
{
    public record Header
    {
        public Header(string controller, string text)
        {
            Controller = controller;
            Text = text;
        }
        public string Controller { get; private init; }
        public string Text { get; private init; }
    }
}