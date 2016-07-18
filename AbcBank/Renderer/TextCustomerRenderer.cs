using System;
using System.Text;

namespace AbcBank.Renderer
{
    /// <summary>
    /// Renders Customer statement in text
    /// </summary>
    public class TextCustomerRenderer
    {
        /// <summary>
        /// Renders the statement for specified customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        public string Render(Customer customer)
        {
            StringBuilder statement = new StringBuilder();
            statement.Append("Statement for " + customer.getName() + "\n");
            foreach (Account account in customer.Accounts)
                statement.Append("\n" + statementForAccount(account) + "\n");
            statement.Append("\nTotal In All Accounts " + toDollars(customer.Balance));
            return statement.ToString();
        }
        private String statementForAccount(Account a)
        {
            String s = "";

            //Translate to pretty account type
            switch (a.AccountType)
            {
                case AccountType.CHECKING:
                    s += "Checking Account\n";
                    break;
                case AccountType.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case AccountType.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.Transactions)
            {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + toDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + toDollars(total);
            return s;
        }

        private String toDollars(double d)
        {
            return String.Format("${0:N2}", Math.Abs(d));
        }

    }
}
