using System.Reflection;

namespace Nonagon.Reflection
{
	/// <summary>
	/// Nonagon IDuplicable extensions.
	/// </summary>
	public static class IDuplicableExtensions
	{
		/// <summary>
		/// Clones the properties.
		/// </summary>
		/// <returns>The properties.</returns>
		/// <param name="owner">Owner which will have the same value of all properties from target.</param>
		/// <param name="target">Target object.</param>
		/// <typeparam name="T">The type of object.</typeparam>
		public static T TakeProperties<T>(this T owner, T target)
			where T : IDuplicable
		{
			var properties = owner.GetType().GetProperties(
				BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			foreach(var property in properties)
			{
				if(property.CanWrite)
					property.SetValue(owner, property.GetValue(target, null), null);
			}

			return owner;
		}
	}
}

