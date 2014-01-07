using System;
using System.Text.RegularExpressions;

namespace Nonagon.Text
{
	public static class StringExtensions
	{
		/// <summary>
		/// Capitalize the specified sentenses.
		/// </summary>
		/// <param name="sentenses">Sentenses to be capitalized.</param>
		public static String Capitalize(this String sentenses)
		{
			var s = Regex.Replace(sentenses, @"\b(\w)", m => m.Value.ToUpper());
			return Regex.Replace(s, @"\s(of|in|by|and)\s", m => m.Value.ToLower(), RegexOptions.IgnoreCase);
		}
	}
}

