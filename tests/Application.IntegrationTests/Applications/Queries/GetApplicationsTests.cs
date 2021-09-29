using AuthorizationServer.Application.TodoLists.Queries.GetApplications;
using AuthorizationServer.Domain.Entities;
using AuthorizationServer.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Application.IntegrationTests.TodoLists.Queries
{
    using static Testing;

    public class GetApplicationsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnPriorityLevels()
        {
            var query = new GetApplicationsQuery();

            var result = await SendAsync(query);

            result.PriorityLevels.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllListsAndItems()
        {
            await AddAsync(new TodoList
            {
                Title = "Shopping",
                Colour = Colour.Blue,
                Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" }
                    }
            });

            var query = new GetApplicationsQuery();

            var result = await SendAsync(query);

            result.Lists.Should().HaveCount(1);
            result.Lists.First().Items.Should().HaveCount(7);
        }
    }
}
