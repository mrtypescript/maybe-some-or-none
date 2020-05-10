using System.Threading.Tasks;
using Xunit;

namespace MaybeSome.Test
{
	public class UnitTest1
	{
		class Person { }

		[Fact]
		public void UserSomeDirectlyShouldBeCompatibleWithNone()
		{
			IMaybe<Person> maybe = new Some<Person>(new Person());
			Assert.True(maybe.HasValue);
			maybe = new None<Person>();
			Assert.False(maybe.HasValue);
		}

		[Fact]
		public void UseProxyClassShouldBeEasyToUse()
		{
			var maybe = Maybe.None<Person>();
			Assert.False(maybe.HasValue);
			maybe = Maybe.Some(new Person());
			Assert.True(maybe.HasValue);
		}

		[Fact]
		public void UseProxyShouldNotAlterReference()
		{
			var person = new Person();
			var maybe = Maybe.Some(person);
			Assert.Equal(person, maybe.Value);
		}

		[Fact]
		public async Task HandleSomeAsyncShould()
		{
			var person = new Person();
			var maybe = Maybe.Some(person);

			var result = await maybe.HandleAsync(async value => { await Task.Delay(1); }, async () => { await Task.Delay(1); });

			Assert.Equal(result.Value, maybe.Value);
		}
		[Fact]
		public void HandleSomeShould()
		{
			var person = new Person();
			var maybe = Maybe.Some(person);

			var result = maybe.Handle(value => { }, () => { });

			Assert.Equal(result.Value, maybe.Value);
		}
	}
}
