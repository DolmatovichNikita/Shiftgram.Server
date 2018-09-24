using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Shiftgram.AccountServer.Controllers;
using Shiftgram.Core.Models;
using Shiftgram.Core.Repository;
using System.Linq;
using System;

namespace Shiftgram.Tests
{
    [TestFixture]
    public class AccountTest
    {
        [Test]
        public async Task TestIsAddAccount()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetAll()).ReturnsAsync(new List<Account>
            {
                new Account {Id = 1}
            });
            AccountController controller = new AccountController(mock.Object);

            Account account = new Account() { FirstName = "TestName" };
            await controller.AddAccount(account);

            mock.Verify(x => x.Add(account), Times.Once);
        }
    }
}
