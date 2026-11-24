namespace Project21
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class Model1 : DbContext
    {
        public Model1() : base("name=Group_FamEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<User> User { get; set; }

        private static Model1 _context;

        public static Model1 GetContext()
        {
            if (_context == null)
                _context = new Model1();
            return _context;
        }

    }
}