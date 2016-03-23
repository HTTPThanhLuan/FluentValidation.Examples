using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicValidation.Model
{
    public interface IFieldValue
    { 
         int FieldId { get; set; }
    }

    public class FieldValue<T> : IFieldValue
    {
        public int FieldId { get; set; }

        public T Value { get; set; }
    }
}
