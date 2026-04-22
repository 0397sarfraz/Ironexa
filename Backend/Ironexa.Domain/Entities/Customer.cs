using System;
using System.Collections.Generic;
using System.Text;

namespace Ironexa.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone {  get; set; }= null!;  
        public string Address { get; set; }=null!;  
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public ICollection<Order> Orders { get; set; }
    }
}
