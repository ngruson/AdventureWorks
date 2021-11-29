﻿using AW.SharedKernel.Interfaces;
using System;

namespace AW.Services.SalesOrder.Core.Idempotency
{
    public class ClientRequest : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}