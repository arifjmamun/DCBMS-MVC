namespace App.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        TestName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Fee = c.Double(nullable: false),
                        TestTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.TestTypes", t => t.TestTypeId, cascadeDelete: true)
                .Index(t => t.TestTypeId);
            
            CreateTable(
                "dbo.TestTypes",
                c => new
                    {
                        TestTypeId = c.Int(nullable: false, identity: true),
                        TestTypeName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.TestTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "TestTypeId", "dbo.TestTypes");
            DropIndex("dbo.Tests", new[] { "TestTypeId" });
            DropTable("dbo.TestTypes");
            DropTable("dbo.Tests");
        }
    }
}
