using System.Configuration;

namespace ConfigurationMapper.Tests.ConfigSections
{
	/// <summary>
	/// Person element collection.
	/// </summary>
	public class PersonElementCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// Creates new Person element.
		/// </summary>
		/// <returns>New Person element.</returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new PersonElement();
		}

		/// <summary>
		/// Returns Id of specified Person element.
		/// </summary>
		/// <param name="element">Person element.</param>
		/// <returns>Person Id.</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((PersonElement)element).Id;
		}
	}
}
