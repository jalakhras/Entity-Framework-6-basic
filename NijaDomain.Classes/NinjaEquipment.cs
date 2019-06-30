using NijaDomain.Classes.Enum;
using NijaDomain.Classes.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace NijaDomain.Classes
{
    public class NinjaEquipment : IModificationHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        [Required]
        public Ninja Ninja { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }

}
