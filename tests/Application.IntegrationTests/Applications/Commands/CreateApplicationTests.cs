using AuthorizationServer.Application.Common.Exceptions;
using AuthorizationServer.Application.TodoLists.Commands.CreateApplication;
using AuthorizationServer.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AuthorizationServer.Application.IntegrationTests.TodoLists.Commands
{
    using static Testing;

    public class CreateApplicationTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var command = new CreateApplicationCommand();

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            await SendAsync(new CreateApplicationCommand
            {
                Title = "Shopping"
            });

            var command = new CreateApplicationCommand
            {
                Title = "Shopping"
            };

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateApplication()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateApplicationCommand
            {
                Title = "Tasks"
            };

            var id = await SendAsync(command);

            var list = await FindAsync<TodoList>(id);

            list.Should().NotBeNull();
            list.Title.Should().Be(command.Title);
            list.CreatedBy.Should().Be(userId);
            list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }
    }
}
