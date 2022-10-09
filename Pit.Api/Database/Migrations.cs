using FluentMigrator;

namespace Pit.Database.Migrations;

[Migration(20221009012022)]
public class TabelaProduto : Migration
{
    public override void Down()
    {

    }

    public override void Up()
    {
        Create.Table("produto")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("nome").AsString().NotNullable()
            .WithColumn("descricao").AsString().NotNullable()
            .WithColumn("codigobarras").AsString().NotNullable()
            .WithColumn("valor").AsDecimal().NotNullable()
            .WithColumn("datahoracriado").AsDate().NotNullable()
            .WithColumn("ativo").AsBoolean().NotNullable();
    }
}