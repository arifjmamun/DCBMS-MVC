using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.EntityModels
{
    public class Test
    {
        public int TestId { get; set; }

        [Required]
        [Display(Name = "Test Name")]
        [Column(TypeName = "varchar")]
        [StringLength(50, MinimumLength = 2)]
        public string TestName { get; set; }

        public double Fee { get; set; }

        [Display(Name = "Test Type")]
        public int TestTypeId { get; set; }

        public virtual TestType TestType { get; set; }
    }
}
