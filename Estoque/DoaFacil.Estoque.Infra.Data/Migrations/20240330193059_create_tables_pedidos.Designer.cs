﻿// <auto-generated />
using System;
using DoaFacil.Estoque.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoaFacil.Infra.Data.Migrations
{
    [DbContext(typeof(ProdutoContext))]
    [Migration("20240330193059_create_tables_pedidos")]
    partial class create_tables_pedidos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("MinhaSequencia")
                .StartsAt(1000L);

            modelBuilder.Entity("DoaFacil.Domain.Models.ItemsPedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdPedidoDoacao")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdProduto")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProdutoNome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPedidoDoacao");

                    b.HasIndex("IdProduto");

                    b.ToTable("items_pedido", (string)null);
                });

            modelBuilder.Entity("DoaFacil.Domain.Models.PedidoDoacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

                    b.Property<DateTime>("DataSolicitacao")
                        .HasColumnType("datetime");

                    b.Property<Guid>("IdSolicitante")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusPedido")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdSolicitante");

                    b.ToTable("pedidos_doacao", (string)null);
                });

            modelBuilder.Entity("DoaFacil.Domain.Models.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("produtos", (string)null);
                });

            modelBuilder.Entity("DoaFacil.Domain.Models.Solicitante", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(13)");

                    b.HasKey("Id");

                    b.ToTable("solicitantes", (string)null);
                });

            modelBuilder.Entity("DoaFacil.Domain.Models.ItemsPedido", b =>
                {
                    b.HasOne("DoaFacil.Domain.Models.PedidoDoacao", "PedidoDoacao")
                        .WithMany("ItemsPedido")
                        .HasForeignKey("IdPedidoDoacao")
                        .IsRequired();

                    b.HasOne("DoaFacil.Domain.Models.Produto", "Produto")
                        .WithMany("ItemsPedido")
                        .HasForeignKey("IdProduto")
                        .IsRequired();

                    b.Navigation("PedidoDoacao");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("DoaFacil.Domain.Models.PedidoDoacao", b =>
                {
                    b.HasOne("DoaFacil.Domain.Models.Solicitante", "Solicitante")
                        .WithMany("PedidosDoacao")
                        .HasForeignKey("IdSolicitante")
                        .IsRequired();

                    b.Navigation("Solicitante");
                });

            modelBuilder.Entity("DoaFacil.Domain.Models.PedidoDoacao", b =>
                {
                    b.Navigation("ItemsPedido");
                });

            modelBuilder.Entity("DoaFacil.Domain.Models.Produto", b =>
                {
                    b.Navigation("ItemsPedido");
                });

            modelBuilder.Entity("DoaFacil.Domain.Models.Solicitante", b =>
                {
                    b.Navigation("PedidosDoacao");
                });
#pragma warning restore 612, 618
        }
    }
}
