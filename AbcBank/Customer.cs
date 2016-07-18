using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank
{
    public class Customer
    {
        /// <summary>
        /// Gets the list of all customer accounts.
        /// </summary>
        /// <value>
        /// The accounts.
        /// </value>
        public IEnumerable<Account> Accounts
        {
            get { return accounts.ToArray(); }
        }

        /// <summary>
        /// Total Customer balance across all accounts
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        public double Balance
        {
            get
            {
                return Accounts.Sum(a => a.sumTransactions());
            }
        }
        private String name;
        private List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String getName()
        {
            return name;
        }

        public Customer openAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public int getNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double totalInterestEarned()
        {
            return Accounts.Sum(a => a.interestEarned());
        }

        public void transfer(double amount, Account from, Account to)
        {   
            if(!accounts.Contains(from))
                throw new ArgumentException("Source account does not belong to the customer");
            if (!accounts.Contains(to))
                throw new ArgumentException("Target account does not belong to the customer");
            to.deposit(from.withdraw(amount));
        }
    }
}
