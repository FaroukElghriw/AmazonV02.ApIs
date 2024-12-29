using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Entites.Order_Aggregate
{
	public class Address
	{
        public Address()
        {
            
        }
        public Address(string fname, string lname, string city, string country , string street)
        {
            FName = fname;
            LName = lname;
            City = city;
            Country = country;
            Street = street;
        }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
