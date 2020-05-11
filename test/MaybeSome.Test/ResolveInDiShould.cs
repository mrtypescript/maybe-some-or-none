using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MaybeSome.Test
{
	public class ResolveInDiShould
	{
		class House
		{
			private readonly IMaybe<Person> _person;

			public House(IMaybe<Person> person)
			{
				_person = person;
			}
		}
		class Person { }

		[Fact]
		public void ResolveSome()
		{
			var services = new ServiceCollection();

			var personToResolve = new Person();
			var somePersonToResolve = Maybe.Some(new Person());

			services.AddSingleton<House>();

			services.AddSingleton(somePersonToResolve);
			services.AddSingleton(personToResolve);

			var serviceProvider = services.BuildServiceProvider();

			var resolvedPerson = serviceProvider.GetRequiredService<Person>();
			var resolvedMaybePerson = serviceProvider.GetRequiredService<IMaybe<Person>>();

			var resolvedHouse = serviceProvider.GetRequiredService<House>();

			Assert.Equal(personToResolve, resolvedPerson);
			Assert.Equal(somePersonToResolve, resolvedMaybePerson);

			Assert.NotNull(resolvedHouse);
		}

		[Fact]
		public void ResolveNone()
		{
			var services = new ServiceCollection();

			services.AddSingleton(Maybe.None<Person>());

			var sp = services.BuildServiceProvider();

			var resolvedNonePerson = sp.GetRequiredService<IMaybe<Person>>();

			Assert.False(resolvedNonePerson.HasValue);
		}
	}
}
