using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestApplication.DataModel.Models
{
    public class Hunter
    {
        [Key]
        public long ID { get; set; }
        
        public string Name { get; set; }

        public long EquippedWeaponID { get; set; }

        [ForeignKey("EquippedWeaponID")]
        public Weapon EquippedWeapon { get; set; }
    }
}
