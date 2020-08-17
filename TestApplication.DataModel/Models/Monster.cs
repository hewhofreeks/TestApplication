using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestApplication.DataModel.Models
{
    public class Monster
    {
        [Key]
        public long ID { get; set; }
        public string Name {get;set;}
        public int Health { get; set; }
    }
}
