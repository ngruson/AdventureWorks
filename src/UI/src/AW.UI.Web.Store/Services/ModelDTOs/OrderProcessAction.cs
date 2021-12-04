namespace AW.UI.Web.Store.Services.ModelDTOs
{
    public record OrderProcessAction
    {
        public string Code { get; }
        public string Name { get; }

        private static readonly OrderProcessAction orderProcessAction = new(nameof(Ship).ToLowerInvariant(), "Ship");
        public static OrderProcessAction Ship = orderProcessAction;

        protected OrderProcessAction()
        {
        }

        public OrderProcessAction(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}