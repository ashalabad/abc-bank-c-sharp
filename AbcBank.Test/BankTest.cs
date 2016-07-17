using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

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
        public void customerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.openAccount(new Account(Account.CHECKING));
            bank.addCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.customerSummary());
        }

        [Test]
        public void checkingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").openAccount(checkingAccount);
            bank.addCustomer(bill);

            checkingAccount.deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));

            checkingAccount.deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void maxi_savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.addCustomer(new Customer("Bill").openAccount(checkingAccount));
            checkingAccount.deposit(3000.0);
            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [Test]
        public void TestTotalInterestPaidOnMixedAccountsSingleCustomer()
        {
            Bank bank = new Bank();
            Account saving, maxiSaving, checking;
            Customer bill=new Customer("Bill")
                .openAccount(checking=new Account(Account.CHECKING))
                .openAccount(maxiSaving=new Account(Account.MAXI_SAVINGS))
                .openAccount(saving=new Account(Account.SAVINGS));            
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
                .openAccount(billChecking = new Account(Account.CHECKING))
                .openAccount(billMaxiSaving = new Account(Account.MAXI_SAVINGS))
                .openAccount(billSaving = new Account(Account.SAVINGS));
            bank.addCustomer(bill);
            Account oscarSaving, oscarMaxiSaving, oscarChecking;
            Customer oscar = new Customer("Oscar")
                .openAccount(oscarChecking = new Account(Account.CHECKING))
                .openAccount(oscarMaxiSaving = new Account(Account.MAXI_SAVINGS))
                .openAccount(oscarSaving = new Account(Account.SAVINGS));
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
