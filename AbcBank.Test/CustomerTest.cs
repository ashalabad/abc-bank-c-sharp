using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {
        [Test]
        public void TestNewCustomerShouldNotHaveAccounts()
        {
            Customer henry = new Customer("Henry");
            Assert.AreEqual("Henry",henry.getName());
            Assert.AreEqual(0,henry.getNumberOfAccounts());
        }
        [Test] //Test customer statement generation
        public void TestRenderCustomerStatement()
        {

            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

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
                    "Total In All Accounts $3,900.00", henry.getStatement());
        }

        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }

        [Test]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                .openAccount(new Account(Account.SAVINGS))
                .openAccount(new Account(Account.CHECKING))
                .openAccount(new Account(Account.MAXI_SAVINGS));
                    
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
        }


        [Test]
        public void TestTotalInterestEarningOnAllAccounts()
        {
            Customer oscar = new Customer("Oscar");
            Account checking=new Account(Account.CHECKING);
            Account saving=new Account(Account.SAVINGS);
            Account maxiSaving = new Account(Account.SAVINGS);
            oscar.openAccount(checking).openAccount(saving).openAccount(maxiSaving);
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
            checking.deposit(1000);
            saving.deposit(2000);
            maxiSaving.deposit(3000);
            double checkingInterest = checking.interestEarned();
            double savingInterest = saving.interestEarned();
            double maxiSavingInterest = maxiSaving.interestEarned();
            Assert.AreEqual(checkingInterest + savingInterest + maxiSavingInterest, oscar.totalInterestEarned(), 1e-15);
        }

    }
}
