using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;
        private IAccountFactory accountFactory=new AccountFactory(DateProvider.getInstance());

        [Test]
        public void TestFirstCustomerThrowsExceptionIfNoCustomersExist()
        {
            Bank bank = new Bank();
            Assert.Throws<System.ArgumentException>(()=> 
            {
                bank.getFirstCustomer();
            });
        }
        [Test]
        public void TestFirstCustomerMustBeJohn()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            bank.addCustomer(john);
            bank.addCustomer(new Customer("Henry"));
            string firstCustomerName = bank.getFirstCustomer();
            Assert.AreEqual("John",firstCustomerName);
        }

        [Test]
        public void checkingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = accountFactory.CreateAccount(AccountType.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);

            checkingAccount.deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings_account()
        {
            Bank bank = new Bank();
            Account savingsAccount = accountFactory.CreateAccount(AccountType.SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(savingsAccount));

            savingsAccount.deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = accountFactory.CreateAccount(AccountType.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(maxiSavingsAccount));
            maxiSavingsAccount.deposit(3000.0);
            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void TestTotalInterestPaidOnMixedAccountsSingleCustomer()
        {
            Bank bank = new Bank();
            Account saving, maxiSaving, checking;
            Customer bill=new Customer("Bill")
                .openAccount(checking = accountFactory.CreateAccount(AccountType.CHECKING))
                .openAccount(maxiSaving = accountFactory.CreateAccount(AccountType.MAXI_SAVINGS))
                .openAccount(saving = accountFactory.CreateAccount(AccountType.SAVINGS));            
            bank.addCustomer(bill);
            saving.deposit(3000);
            checking.deposit(1000);
            maxiSaving.deposit(4000);
            double billInterest = bill.totalInterestEarned();
            Assert.AreEqual(billInterest,bank.totalInterestPaid());
        }

        [Test]
        public void TestTotalInterestPaidOnMixedAccountsMultipleCustomers()
        {
            Bank bank = new Bank();
            Account billSaving, billMaxiSaving, billChecking;
            Customer bill = new Customer("Bill")
                .openAccount(billChecking = accountFactory.CreateAccount(AccountType.CHECKING))
                .openAccount(billMaxiSaving = accountFactory.CreateAccount(AccountType.MAXI_SAVINGS))
                .openAccount(billSaving = accountFactory.CreateAccount(AccountType.SAVINGS));
            bank.addCustomer(bill);
            Account oscarSaving, oscarMaxiSaving, oscarChecking;
            Customer oscar = new Customer("Oscar")
                .openAccount(oscarChecking = accountFactory.CreateAccount(AccountType.CHECKING))
                .openAccount(oscarMaxiSaving = accountFactory.CreateAccount(AccountType.MAXI_SAVINGS))
                .openAccount(oscarSaving = accountFactory.CreateAccount(AccountType.SAVINGS));
            bank.addCustomer(oscar);
            billSaving.deposit(3000);
            billChecking.deposit(1000);
            billMaxiSaving.deposit(4000);
            double billInterest = bill.totalInterestEarned();
            oscarSaving.deposit(3000);
            oscarChecking.deposit(1000);
            oscarMaxiSaving.deposit(4000);
            double oscarInterest = oscar.totalInterestEarned();
            Assert.AreEqual(oscarInterest+billInterest,bank.totalInterestPaid());
        }
    }
}
