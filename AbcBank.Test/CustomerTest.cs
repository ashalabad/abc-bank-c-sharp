using System;
using System.Linq;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {
        private readonly IAccountFactory accountFactory=new AccountFactory(DateProvider.getInstance());
        [Test]
        public void TestNewCustomerShouldNotHaveAccounts()
        {
            Customer henry = new Customer("Henry");
            Assert.AreEqual("Henry",henry.getName());
            Assert.AreEqual(0,henry.getNumberOfAccounts());
        }

        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(accountFactory.CreateAccount(AccountType.SAVINGS));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(accountFactory.CreateAccount(AccountType.SAVINGS));
            oscar.openAccount(accountFactory.CreateAccount(AccountType.CHECKING));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }

        [Test]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                .openAccount(accountFactory.CreateAccount(AccountType.SAVINGS))
                .openAccount(accountFactory.CreateAccount(AccountType.CHECKING))
                .openAccount(accountFactory.CreateAccount(AccountType.MAXI_SAVINGS));
                    
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
            Assert.AreEqual(3,oscar.Accounts.Count());
        }

        [Test]
        public void TestTotalBalanceOnAllAccounts()
        {
            Account checking, saving, maxisaving;
            Customer oscar = new Customer("Oscar")
                .openAccount(saving=accountFactory.CreateAccount(AccountType.SAVINGS))
                .openAccount(checking=accountFactory.CreateAccount(AccountType.CHECKING))
                .openAccount(maxisaving=accountFactory.CreateAccount(AccountType.MAXI_SAVINGS));
            checking.deposit(1000);
            saving.deposit(1000);
            maxisaving.deposit(1000);
            saving.withdraw(500);
            Assert.AreEqual(2500,oscar.Balance);
        }

        [Test]
        public void TestTransferBetweenAccount()
        {
            Account checking, saving;
            Customer oscar = new Customer("Oscar")
                .openAccount(saving = accountFactory.CreateAccount(AccountType.SAVINGS))
                .openAccount(checking = accountFactory.CreateAccount(AccountType.CHECKING));
            saving.deposit(1000);
            oscar.transfer(500,saving,checking);
            Assert.AreEqual(1000, oscar.Balance);
            Assert.AreEqual(500,saving.sumTransactions());
            Assert.AreEqual(500, checking.sumTransactions());

        }

        [Test]
        public void TestTransferBetweenAccountInsufficientFunds()
        {
            Account checking, saving;
            Customer oscar = new Customer("Oscar")
                .openAccount(saving = accountFactory.CreateAccount(AccountType.SAVINGS))
                .openAccount(checking = accountFactory.CreateAccount(AccountType.CHECKING));
            saving.deposit(1000);
            Assert.Throws<InsufficientFundsException>(() =>
            {
                oscar.transfer(5000, saving, checking);
            });
            
        }

        [Test]
        public void TestTransferBetweenAccountAccountFromDoesNotBelongToCustomer()
        {
            Account checking, saving = accountFactory.CreateAccount(AccountType.SAVINGS);
            Customer oscar = new Customer("Oscar").openAccount(checking = accountFactory.CreateAccount(AccountType.CHECKING));
            saving.deposit(1000);
            Assert.Throws<ArgumentException>(() =>
            {
                oscar.transfer(500, saving, checking);    
            });
        }

        [Test]
        public void TestTransferBetweenAccountAccountToDoesNotBelongToCustomer()
        {
            Account checking, saving = accountFactory.CreateAccount(AccountType.SAVINGS);
            Customer oscar = new Customer("Oscar").openAccount(checking = accountFactory.CreateAccount(AccountType.CHECKING));
            checking.deposit(1000);
            Assert.Throws<ArgumentException>(() =>
            {
                oscar.transfer(500, checking, saving);
            });

        }
        [Test]
        public void TestTotalInterestEarningOnAllAccounts()
        {
            Customer oscar = new Customer("Oscar");
            Account checking = accountFactory.CreateAccount(AccountType.CHECKING);
            Account saving = accountFactory.CreateAccount(AccountType.SAVINGS);
            Account maxiSaving = accountFactory.CreateAccount(AccountType.MAXI_SAVINGS);
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
