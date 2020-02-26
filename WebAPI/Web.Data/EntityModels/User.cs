namespace Web.Data.EntityModels
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}
