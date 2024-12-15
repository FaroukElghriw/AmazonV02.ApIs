﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonV02.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class changenameofcolFkofBrandandtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brnands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Types_TypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Products",
                newName: "ProductTypeId");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "ProductBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                newName: "IX_Products_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_ProductBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brnands_ProductBrandId",
                table: "Products",
                column: "ProductBrandId",
                principalTable: "Brnands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Types_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brnands_ProductBrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Types_ProductTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "Products",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "ProductBrandId",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                newName: "IX_Products_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brnands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brnands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Types_TypeId",
                table: "Products",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
