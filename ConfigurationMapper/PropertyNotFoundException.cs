using System;

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
