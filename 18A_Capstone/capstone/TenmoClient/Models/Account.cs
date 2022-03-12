namespace TenmoClient.Models
{
    /// <summary>
    /// Return value from login endpoint
    /// </summary>
    public class Account
    {
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string Username { get; set; }
    }
}
