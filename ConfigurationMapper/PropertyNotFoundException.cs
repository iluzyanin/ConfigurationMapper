using System;

namespace ConfigurationMapper
{
	/// <summary>
	/// Property not found exception. Thrown when property is required but has no value.
	/// </summary>
    public sealed class PropertyNotFoundException: Exception
    {
        public PropertyNotFoundException(string propertyName)
            : base(String.Format(Errors.PropertyNotFound, propertyName))
        {
            PropertyName = propertyName;
        }

		/// <summary>
		/// Property name.
		/// </summary>
        public string PropertyName { get; set; }
    }
}
