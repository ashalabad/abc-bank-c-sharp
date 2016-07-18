using AbcBank.Renderer;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class TextRendererTest
    {
        [Test] //Test customer statement generation
        public void TestRenderCustomerStatement()
        {

            TextCustomerRenderer renderer=new TextCustomerRenderer();
            IAccountFactory accountFactory=new AccountFactory(DateProvider.getInstance());
            Account checkingAccount = accountFactory.CreateAccount(AccountType.CHECKING);
            Account savingsAccount = accountFactory.CreateAccount(AccountType.SAVINGS);

            Customer henry = new Customer("Henry").openAccount(checkingAccount).openAccount(savingsAccount);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(4000.0);
            savingsAccount.withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", renderer.Render(henry));
        }

        [Test]
        public void TestRenderCustomerSummary()
        {
            TextBankRenderer renderer=new TextBankRenderer();
            Bank bank = new Bank();
            Customer john = new Customer("John");
            IAccountFactory accountFactory = new AccountFactory(DateProvider.getInstance());
            john.openAccount(accountFactory.CreateAccount(AccountType.CHECKING));
            bank.addCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", renderer.Render(bank));
        }


    }
}
