using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FordWare.Migrations
{
    public partial class AddShoppingCartToDbNavRequiredProjectIdNullable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Projects_ProjectId",
                table: "ShoppingCarts");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ShoppingCarts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Projects_ProjectId",
                table: "ShoppingCarts",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Projects_ProjectId",
                table: "ShoppingCarts");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Projects_ProjectId",
                table: "ShoppingCarts",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
