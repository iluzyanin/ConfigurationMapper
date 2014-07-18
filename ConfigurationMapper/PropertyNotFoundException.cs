using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationMapper
{
    public sealed class PropertyNotFoundException: Exception
    {
        public PropertyNotFoundException(string propertyName)
            : base(String.Format(Errors.PropertyNotFound, propertyName))
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }
}
