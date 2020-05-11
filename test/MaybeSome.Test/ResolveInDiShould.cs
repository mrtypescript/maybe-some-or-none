using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MaybeSome.Test
{
	public class ResolveInDiShould
	{
		class Person { }

		[Fact]
		public void ResolveSome()
		{
			var services = new ServiceCollection();
			var personToResolve = new Person();
			var somePersonToResolve = Maybe.Some(new Person());
			services.AddSingleton(somePersonToResolve);
			services.AddSingleton(personToResolve);
			var serviceProvider = services.BuildServiceProvider();
			var resolvedPerson = serviceProvider.GetRequiredService<Person>();
			var resolvedMaybePerson = serviceProvider.GetRequiredService<IMaybe<Person>>();
			Assert.Equal(personToResolve, resolvedPerson);
			Assert.Equal(somePersonToResolve, resolvedMaybePerson);
		}
	}
}
