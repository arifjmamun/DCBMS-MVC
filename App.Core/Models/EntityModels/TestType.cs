using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.EntityModels
{
    public class TestType
    {
        [Display(Name = "Test Type")]
        public int TestTypeId { get; set; }
        
        [Required]
        [Display(Name = "Test Type")]
        [Column(TypeName = "varchar")]
        [StringLength(50, MinimumLength = 2)]
        public string TestTypeName { get; set; }

        public virtual List<Test> Tests { get; set; }
    }
}
