using System;
using System.Text;

namespace AbcBank.Renderer
{
    /// <summary>
    /// Renders Bank summary in text
    /// </summary>
    public class TextBankRenderer
    {
        /// <summary>
        /// Renders the bank summary in test
        /// </summary>
        /// <param name="bank">The bank.</param>
        /// <returns></returns>
        public string Render(Bank bank)
        {
            StringBuilder summary=new StringBuilder();
            summary.Append("Customer Summary");
            foreach (Customer c in bank.Customers)
                summary.Append("\n - " + c.getName() + " (" + format(c.getNumberOfAccounts(), "account") + ")");
            return summary.ToString();
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private string format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

    }
}
