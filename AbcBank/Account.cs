using AbcBank.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{
    /// <summary>
    /// Defined accout types
    /// </summary>
    public enum AccountType
    {
        CHECKING = 0,
        SAVINGS,
        MAXI_SAVINGS
    }

    public class Account
    {
        public AccountType AccountType {
            get;
            private set;
        }

        public IEnumerable<Transaction> Transactions
        {
            get
            {
                lock(locker)
                    return transactionsList.ToArray();
            }
        } 
        private readonly object locker = new object();
        private readonly List<Transaction> transactionsList;
        private readonly IInterestCalculator interestCalculator;
        private readonly IDateProvider dateProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="accountType">Type of the account.</param>
        /// <param name="interestCalculator">The interest calculator.</param>
        /// <param name="dateProvider">The date provider.</param>
        public Account(AccountType accountType,IInterestCalculator interestCalculator,IDateProvider dateProvider)
        {
            AccountType = accountType;
            transactionsList = new List<Transaction>();
            this.interestCalculator = interestCalculator;
            this.dateProvider = dateProvider;
        }

        /// <summary>
        /// Deposits the specified amount to the account.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns>deposited amount</returns>
        public double deposit(double amount)
        {
            amount.Positive();
            lock (locker)
            {
                transactionsList.Add(new Transaction(amount,dateProvider.now()));
                return amount;
            }
        }

        /// <summary>
        /// Withdraws the specified amount from the account.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Insufficient funds</exception>
        public double withdraw(double amount)
        {
            amount.Positive();
            lock (locker)
            {
                if (sumTransactions() < amount)
                    throw new InsufficientFundsException("Insufficient funds");
                transactionsList.Add(new Transaction(-amount,dateProvider.now()));
                return amount;
            }
        }
        /// <summary>
        /// Calculate earned interest
        /// </summary>
        /// <returns></returns>
        public double interestEarned()
        {
            return interestCalculator.Calculate(Transactions, dateProvider.now());
        }

        public double sumTransactions()
        {
             return Transactions.Sum(t => t.Amount);
        }
    }
}
