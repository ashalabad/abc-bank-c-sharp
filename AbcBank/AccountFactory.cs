using System;
using System.Collections.Generic;
using AbcBank.Rules;

namespace AbcBank
{
    /// <summary>
    /// Account Factory
    /// </summary>
    /// <seealso cref="AbcBank.IAccountFactory" />
    public class AccountFactory : IAccountFactory
    {
        private static readonly Dictionary<AccountType, Func<IDateProvider, Account>>
            accMap = new Dictionary<AccountType, Func<IDateProvider, Account>>{
                { AccountType.CHECKING,p=> new Account(AccountType.CHECKING,new CheckingInterestCalculator(),p)},
                { AccountType.SAVINGS,p=> new Account(AccountType.SAVINGS,new SavingInterestCalculator(),p)},
                { AccountType.MAXI_SAVINGS,p=> new Account(AccountType.MAXI_SAVINGS,new MaxiSavingInterestCalculator(),p)}
              };
        private readonly IDateProvider dateProvider;
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountFactory"/> class.
        /// </summary>
        /// <param name="dateProvider">The date provider.</param>
        public AccountFactory(IDateProvider dateProvider)
        {
            this.dateProvider = dateProvider;
        }
        /// <summary>
        /// Factory method to assemble and return an account based on passed account type.
        /// </summary>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>
        /// new account
        /// </returns>
        /// <exception cref="System.ArgumentException"></exception>
        public Account CreateAccount(AccountType accountType)
        {
            Func<IDateProvider, Account> f;
            if (!accMap.TryGetValue(accountType, out f))
                throw new ArgumentException(string.Format("Cannot instantiate account of type {0}", accountType));
            return f(dateProvider);
        }
    }
}
