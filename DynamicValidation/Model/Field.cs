﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model.ValidationRules;

namespace DynamicValidation.Model
{
    public class Field
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// C# type of the field value
        /// </summary>
        public Type Type { get; set; }

        public IEnumerable<ValidationRule> ValidationRules { get; set; }

        public string ValidationRule { get; set; }
    }
}
