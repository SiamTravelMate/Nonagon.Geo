using System;
using System.Collections.Generic;

namespace Nonagon
{
	/// <summary>
	/// Array Randomizer.
	/// </summary>
	/// <typeparam name="T">Type of element.</typeparam>
	public sealed class ArrayRandomizer<T>
	{
		private readonly List<T> _sourceArray;
		
		// If cryptographically save random numbers are needed 
		// switch to System.Security.Cryptography.RandomNumberGenerator        
		private readonly Random _random;
		
		/// <summary>
		/// Returns a copy of the source array that is used to generate random arrays from.
		/// </summary>
		public List<T> SourceArray
		{
			get { return new List<T>(_sourceArray); }
		}
		
		/// <summary>
		/// Construct an array randomizer, drawing random numbers from sourceArray.
		/// </summary>
		/// <param name="sourceArray">the array the randomizer draws its numbers from</param>
		public ArrayRandomizer(List<T> sourceArray)
		{
			_random = new Random();
			_sourceArray = sourceArray;
		}
		
		#region Select and Remove Recursion
		
		/// <summary>
		/// Returns a random draw of entries of SourceArray of random length. 
		/// Any specific entry in SourceArray can at most be used once in 
		/// the resulting random array. The returned array is construced 
		/// using a recursive approach.
		/// </summary>
		/// <returns>random draw of random length</returns>
		public List<T> GetRandomSubsetRecursion()
		{
			int size = _random.Next(_sourceArray.Count);
			return GetRandomSubsetRecursion(size);
		}
		
		/// <summary>
		/// Returns a random draw of entries of SourceArray of length size. 
		/// Any specific entry in SourceArray can at most be used once in 
		/// the resulting random array. The returned array is construced 
		/// using a recursive approach.
		/// </summary>
		/// <param name="size">size of array to return.</param>
		/// <returns>random draw of length size</returns>
		public List<T> GetRandomSubsetRecursion(int size)
		{
			if(size > _sourceArray.Count)
			{
				throw new ArgumentException("Size can't be larger than count of elements in SourceArray", "size");
			}
			var target = new List<T>();
			GetRandomSubsetRecursion(SourceArray, target, size);
			return target;
		}
		
		
		private void GetRandomSubsetRecursion(List<T> source, List<T> target, int size)
		{
			if(size > 0)
			{
				var randomElement = _random.Next(source.Count);
				T element = source[randomElement];
				source.RemoveAt(randomElement);
				target.Add(element);
				GetRandomSubsetRecursion(source, target, size - 1);
			}
		}
		
		#endregion
		
		#region Select and Remove
		
		/// <summary>
		/// Returns a random draw of entries of SourceArray of random length. 
		/// Any specific entry in SourceArray can at most be used once in 
		/// the resulting random array. The returned array is construced 
		/// using a recursive approach.
		/// </summary>
		/// <returns>random draw of random length</returns>
		public List<T> GetRandomSubsetSelectAndRemove()
		{
			int size = _random.Next(_sourceArray.Count);
			return GetRandomSubsetSelectAndRemove(size);
		}
		
		/// <summary>
		/// Returns a random draw of entries of SourceArray of length size. 
		/// Any specific entry in SourceArray can at most be used once in 
		/// the resulting random array. The returned array is construced 
		/// using a recursive approach.
		/// </summary>
		/// <param name="size">size of array to return.</param>
		/// <returns>random draw of length size</returns>
		public List<T> GetRandomSubsetSelectAndRemove(int size)
		{
			if(size > _sourceArray.Count)
			{
				throw new ArgumentException("Size can't be larger than count of elements in SourceArray", "size");
			}
			
			var source = SourceArray;
			var target = new List<T>(size);
			
			for(int i = 0; i < size; i++)
			{
				int randomElement = _random.Next(source.Count);
				T element = source[randomElement];
				source.RemoveAt(randomElement);
				target.Add(element);
			}
			return target;
		}
		
#endregion
		
		#region FisherYatesShuffle
		
		/// <summary>
		/// Returns a random draw of entries of SourceArray of random length. 
		/// Any specific entry in SourceArray can at most be used once in 
		/// the resulting random array. The returned array is construced 
		/// using the Fisher-Yates shuffle algorithm.
		/// http://www.nist.gov/dads/HTML/fisherYatesShuffle.html
		/// </summary>
		/// <returns>random draw of random length</returns>
		public List<T> GetRandomSubsetFisherYates()
		{
			int size = _random.Next(_sourceArray.Count);
			return GetRandomSubsetFisherYates(size);
		}
		
		/// <summary>
		/// Returns a random draw of entries of SourceArray of random length. 
		/// Any specific entry in SourceArray can at most be used once in 
		/// the resulting random array. The returned array is construced 
		/// using the Fisher-Yates shuffle algorithm.
		/// http://www.nist.gov/dads/HTML/fisherYatesShuffle.html
		/// </summary>
		/// <param name="size">size of array to return.</param>
		/// <returns>random draw of length size</returns>
		public List<T> GetRandomSubsetFisherYates(int size)
		{
			if(size > _sourceArray.Count)
			{
				throw new ArgumentException("Size can't be larger than count of elements in SourceArray", "size");
			}
			if(size < 0)
			{
				// to be consistent with recursive version, maybe return a zero-size list?
				throw new ArgumentException("Size can't be negative", "size");
			}
			
			List<T> randomArray = SourceArray;
			if(size > 0)
			{
				for(int i = 0; i < size - 1; i++)
				{
					int swapPosition = i + _random.Next(size - i);
					T swap = randomArray[swapPosition];
					randomArray[swapPosition] = randomArray[i];
					randomArray[i] = swap;
				}
			}
			
			var targetArray = new T[size];
			randomArray.CopyTo(0, targetArray, 0, size);
			return new List<T>(targetArray);
		}
		
		#endregion
	}
}
