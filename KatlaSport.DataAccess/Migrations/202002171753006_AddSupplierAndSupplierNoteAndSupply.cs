namespace KatlaSport.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Add new table supplier, supplier note and supply
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class AddSupplierAndSupplierNoteAndSupply : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.product_supplies",
                c => new
                    {
                        supplier_id = c.Int(nullable: false),
                        catalogue_product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.supplier_id, t.catalogue_product_id })
                .ForeignKey("dbo.catalogue_products", t => t.catalogue_product_id, cascadeDelete: true)
                .ForeignKey("dbo.product_suppliers", t => t.supplier_id, cascadeDelete: true)
                .Index(t => t.supplier_id)
                .Index(t => t.catalogue_product_id);

            CreateTable(
                "dbo.product_suppliers",
                c => new
                    {
                        product_supplier_id = c.Int(nullable: false, identity: true),
                        product_supplier_company_name = c.String(nullable: false, maxLength: 60),
                        product_supplier_address = c.String(nullable: false, maxLength: 300),
                        product_supplier_phone = c.String(nullable: false, maxLength: 20),
                        deleted = c.Boolean(nullable: false),
                        created_utc = c.DateTime(nullable: false),
                        updated_utc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.product_supplier_id);

            CreateTable(
                "dbo.product_supplier_notes",
                c => new
                    {
                        product_supplier_notes_id = c.Int(nullable: false, identity: true),
                        product_supplier_notes_parent_id = c.Int(),
                        product_supplier_notes_note = c.String(maxLength: 500),
                        deleted = c.Boolean(nullable: false),
                        product_supplier_id = c.Int(nullable: false),
                        created_utc = c.DateTime(nullable: false),
                        updated_utc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.product_supplier_notes_id)
                .ForeignKey("dbo.product_suppliers", t => t.product_supplier_id, cascadeDelete: true)
                .Index(t => t.product_supplier_id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.product_supplies", "supplier_id", "dbo.product_suppliers");
            DropForeignKey("dbo.product_supplier_notes", "product_supplier_id", "dbo.product_suppliers");
            DropForeignKey("dbo.product_supplies", "catalogue_product_id", "dbo.catalogue_products");
            DropIndex("dbo.product_supplier_notes", new[] { "product_supplier_id" });
            DropIndex("dbo.product_supplies", new[] { "catalogue_product_id" });
            DropIndex("dbo.product_supplies", new[] { "supplier_id" });
            DropTable("dbo.product_supplier_notes");
            DropTable("dbo.product_suppliers");
            DropTable("dbo.product_supplies");
        }
    }
}
