﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Infrastructure.Contexts
{
    internal sealed class CorrelationContext
    {
        public string CorrelationId { get; set; }
        public string SpanContext { get; set; }
        public UserContext User { get; set; }
        public string ResourceId { get; set; }
        public string TraceId { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public class UserContext
        {
            public string Id { get; set; }
            public bool IsAuthenticated { get; set; }
            public string Role { get; set; }
            public IDictionary<string, string> Claims { get; set; }
        }
    }
}
