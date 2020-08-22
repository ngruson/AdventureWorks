﻿using AW.Domain.Sales;
using MediatR;

namespace AW.Application.CountCustomers
{
    public class CountCustomersQuery : IRequest<int>
    {
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
    }
}