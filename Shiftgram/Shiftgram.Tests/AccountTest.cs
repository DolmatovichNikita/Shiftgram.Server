using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Shiftgram.AccountServer.Controllers;
using Shiftgram.Core.Models;
using Shiftgram.Core.Repository;
using System.Web.Http.Results;

namespace Shiftgram.Tests
{
	[TestFixture]
    public class AccountTest
    {
        /*[Test]
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
        }*/

        [Test]
        public async Task TestDeleteAccount()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetAll()).ReturnsAsync(new List<Account>
            {
                new Account {Id = 1},
                new Account {Id = 2}
            });
            AccountController controller = new AccountController(mock.Object);

            var actionResultOk = await controller.DeleteAccount(1);

            Assert.AreEqual(actionResultOk.GetType(), typeof(OkResult));
        }

        [Test]
        public async Task TestGetAllAccounts()
        {
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(x => x.GetAll()).ReturnsAsync(new List<Account>
            {
                new Account {Id = 1},
                new Account {Id = 2}
            });
            AccountController controller = new AccountController(mock.Object);

            var accounts = await controller.GetAllAccounts() as List<Account>;

            Assert.AreEqual(accounts.Count, 2);
        }
    }
}
