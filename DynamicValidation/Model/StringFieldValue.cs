using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicValidation.Model
{
    public class StringFieldValue: FieldValue
    {
        public StringField Field { get; set; }

        public string Value { get; set; }
    }
}
