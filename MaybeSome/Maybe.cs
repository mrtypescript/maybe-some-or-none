using System;
using System.Threading.Tasks;

namespace MaybeSome
{
	public static class MaybeExtensions
	{
		public static async Task<IMaybe<TValue>> HandleAsync<TValue>(this IMaybe<TValue> item, Func<TValue, Task> onSome, Func<Task> onNone)
		{
			if (item.HasValue)
				await onSome(item.Value);
			await onNone();
			return item;
		}
		public static IMaybe<TValue> Handle<TValue>(this IMaybe<TValue> item, Action<TValue> onSome, Action onNone)
		{
			if (item.HasValue)
				onSome(item.Value);
			onNone();
			return item;
		}
	}
	public static class Maybe
	{
		public static IMaybe<TValue> Some<TValue>(TValue value) => MaybeSome.Some.AsMaybe(value);
		public static IMaybe<TValue> None<TValue>() => MaybeSome.None.AsMaybe<TValue>();
	}

	public interface IMaybe<out TValue>
	{
		bool HasValue { get; }

		TValue Value { get; }
	}

	public class Some
	{
		public static IMaybe<TValue> AsMaybe<TValue>(TValue value)
		{
			return new Some<TValue>(value);
		}
	}

	public class Some<TMaybe> : IMaybe<TMaybe>
	{
		public bool HasValue { get; } = true;
		public Some(TMaybe maybe)
		{
			Value = maybe;
		}
		public TMaybe Value { get; }
	}

	public class None
	{
		public static IMaybe<TValue> AsMaybe<TValue>() => new None<TValue>();
	}

	public class None<TMaybe> : IMaybe<TMaybe>
	{
		public bool HasValue { get; } = false;
		public TMaybe Value { get; }
	}
}
