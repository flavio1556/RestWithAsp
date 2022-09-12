using RestWithASPNET.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace RestWithASPNET.Models.Entites
{
    [Table("person")]
    public class Person : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("gender")]
        public string Gender { get; set; }
        [Column("enabled")]
        public bool Enabled { get; set; }

        public Person(string firstName,string lastname, string address, string gender)
        {
            FirstName = firstName;
            LastName = lastname;
            Address = address;  
            Gender = gender;       
            
        }
        public Person()
        {

        }
    }
}
