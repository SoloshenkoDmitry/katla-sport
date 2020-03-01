namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Added a new field "avatar" to the table suppliers
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class AddAvatarInSupplier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.product_suppliers", "avatar", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.product_suppliers", "avatar");
        }
    }
}
