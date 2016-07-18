using System;
using System.Collections.Generic;

namespace AbcBank
{
    public class Bank
    {
        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        public IEnumerable<Customer> Customers
        {
            get { return customers.ToArray(); }
        }
        private List<Customer> customers;

        public Bank()
        {
            customers = new List<Customer>();
        }

        public void addCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public double totalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in customers)
                total += c.totalInterestEarned();
            return total;
        }

        public String getFirstCustomer()
        {
            if (customers.Count == 0)
                throw new ArgumentException("The bank has no customers");
            return customers[0].getName();
        }
    }
}
