using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model.ValidationRule;

namespace DynamicValidation.Model
{
    public interface IField
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    public class Field<T>: IField
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<IValidationRule> ValidationRules { get; set; }
    }
}
