namespace TenmoClient.Models
{
    /// <summary>
    /// Model to provide login parameters
    /// </summary>
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
