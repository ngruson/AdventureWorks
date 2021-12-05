namespace AW.UI.Web.Store.Services.ModelDTOs
{
    public record OrderProcessAction
    {
        public string Code { get; }
        public string Name { get; }

        public static OrderProcessAction Ship { get; private set; }

        protected OrderProcessAction()
        {
        }

        static OrderProcessAction()
        {
            Ship = new(nameof(Ship).ToLowerInvariant(), "Ship");
        }

        public OrderProcessAction(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}