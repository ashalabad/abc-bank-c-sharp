using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountFactoryTest
    {
        [Test]
        public void TestCreateCheckingAccount()
        {
            AccountFactory factory = new AccountFactory(DateProvider.getInstance());
            Account account =  factory.CreateAccount(AccountType.CHECKING);
            Assert.IsNotNull(account);
            Assert.AreEqual(AccountType.CHECKING, account.AccountType);
        }
        [Test]
        public void TestCreateSavingAccount()
        {
            AccountFactory factory = new AccountFactory(DateProvider.getInstance());
            Account account = factory.CreateAccount(AccountType.SAVINGS);
            Assert.IsNotNull(account);
            Assert.AreEqual(AccountType.SAVINGS, account.AccountType);

        }
        [Test]
        public void TestCreateMaxiSavingAccount()
        {
            AccountFactory factory = new AccountFactory(DateProvider.getInstance());
            Account account = factory.CreateAccount(AccountType.MAXI_SAVINGS);
            Assert.IsNotNull(account);
            Assert.AreEqual(AccountType.MAXI_SAVINGS, account.AccountType);

        }
    }
}
