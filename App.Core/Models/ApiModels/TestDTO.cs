using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Models.EntityModels;

namespace App.Core.Models.ApiModels
{
    public class TestDTO
    {
        public int TestId { get; set; }

        public string TestName { get; set; }

        public double Fee { get; set; }

        public int TestTypeId { get; set; }

        public string TestTypeName { get; set; }
    }
}
