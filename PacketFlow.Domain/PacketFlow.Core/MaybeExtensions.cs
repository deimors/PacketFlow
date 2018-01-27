using Functional.Maybe;
using System.Collections.Generic;

namespace Workshop.Core
{
	public static class MaybeExtensions
	{
		public static Maybe<TValue> Lookup<TValue, TKey>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) 
			=> (dictionary.TryGetValue(key, out var value))
				? value.ToMaybe()
				: Maybe<TValue>.Nothing;
	}
}
