﻿// <auto-generated />
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiEcommerce2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241031011655_agregarcolumndProductoId")]
    partial class agregarcolumndProductoId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.35");

            modelBuilder.Entity("Domain.Entities.DetalleOrden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OrdenId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductoNombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrdenId");

                    b.ToTable("DetallesOrden");
                });

            modelBuilder.Entity("Domain.Entities.Domain.Entities.Orden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalPagar")
                        .HasColumnType("TEXT");

                    b.Property<string>("UsuarioEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UsuarioNombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ordenes");
                });

            modelBuilder.Entity("Domain.Entities.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Imagen")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Precio")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdminLoggedIn")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Domain.Entities.DetalleOrden", b =>
                {
                    b.HasOne("Domain.Entities.Domain.Entities.Orden", "Orden")
                        .WithMany("DetallesOrden")
                        .HasForeignKey("OrdenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orden");
                });

            modelBuilder.Entity("Domain.Entities.Domain.Entities.Orden", b =>
                {
                    b.Navigation("DetallesOrden");
                });
#pragma warning restore 612, 618
        }
    }
}