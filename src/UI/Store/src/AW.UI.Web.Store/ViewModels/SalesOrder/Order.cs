using AW.UI.Web.Store.ViewModels.Annotations;
using AW.UI.Web.Store.ViewModels.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Store.ViewModels.SalesOrder
{
    public class Order
    {
        [JsonConverter(typeof(NumberToStringConverter))]
        public string OrderNumber { get; set; }
        public string ShipMethod { get; set; }

        public Address BillToAddress { get; set; }
        public Address ShipToAddress { get; set; }
        
        [Required]
        [DisplayName("Card number")]
        public string CardNumber { get; set; }
        [Required]
        [DisplayName("Cardholder name")]
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        [CardExpiration(ErrorMessage = "The card is expired"), Required]
        [DisplayName("Card expiration")]
        public string CardExpirationShort { get; set; }
        [Required]
        [DisplayName("Card security number")]
        public string CardSecurityNumber { get; set; }
        public string CardType { get; set; }

        public string Buyer { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        [Required]
        public Guid RequestId { get; set; }


        public void CardExpirationShortFormat()
        {
            CardExpirationShort = CardExpiration.ToString("MM/yy");
        }

        public void CardExpirationApiFormat()
        {
            var month = CardExpirationShort.Split('/')[0];
            var year = $"20{CardExpirationShort.Split('/')[1]}";

            CardExpiration = new DateTime(int.Parse(year), int.Parse(month), 1);
        }
    }
}