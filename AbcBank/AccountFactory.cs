using System;
using System.Collections.Generic;
using AbcBank.Rules;

namespace AbcBank
{
    /// <summary>
    /// Account Factory interface
    /// </summary>
    public interface IAccountFactory {
        /// <summary>
        /// Creates the account.
        /// </summary>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>new account</returns>
        Account CreateAccount(AccountType accountType);
    }

    /// <summary>
    /// Account Factory
    /// </summary>
    /// <seealso cref="AbcBank.IAccountFactory" />
    public class AccountFactory : IAccountFactory
    {
        private static readonly Dictionary<AccountType, Func<DateProvider, Account>>
            _accMap = new Dictionary<AccountType, Func<DateProvider, Account>>{
                { AccountType.CHECKING,p=> { return new Account(AccountType.CHECKING,new CheckingInterestCalculator(),p); } },
                { AccountType.SAVINGS,p=> { return new Account(AccountType.SAVINGS,new SavingInterestCalculator(),p); } },
                { AccountType.MAXI_SAVINGS,p=> { return new Account(AccountType.MAXI_SAVINGS,new MaxiSavingInterestCalculator(),p); } }
              };
        private readonly DateProvider dateProvider;
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountFactory"/> class.
        /// </summary>
        /// <param name="dateProvider">The date provider.</param>
        public AccountFactory(DateProvider dateProvider)
        {
            this.dateProvider = dateProvider;
        }
        public Account CreateAccount(AccountType accountType)
        {
            Func<DateProvider, Account> f;
            if (!_accMap.TryGetValue(accountType, out f))
                throw new ArgumentException(string.Format("Cannot instantiate account of type {0}", accountType));
            return f(dateProvider);
        }
    }
}
