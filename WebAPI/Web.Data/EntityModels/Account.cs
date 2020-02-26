namespace Web.Data.EntityModels
{
    public class Account
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
