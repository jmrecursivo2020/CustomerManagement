using System;

namespace DataLayerCustomerManagement.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
