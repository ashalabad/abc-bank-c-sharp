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
}