﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using net_il_mio_fotoalbum.Database;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    [DbContext(typeof(FotoContext))]
    partial class FotoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoriaFoto", b =>
                {
                    b.Property<string>("CategorieId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FotosId")
                        .HasColumnType("int");

                    b.HasKey("CategorieId", "FotosId");

                    b.HasIndex("FotosId");

                    b.ToTable("CategoriaFoto");
                });

            modelBuilder.Entity("net_il_mio_fotoalbum.Models.Categoria", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categorie");
                });

            modelBuilder.Entity("net_il_mio_fotoalbum.Models.Foto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("descrizione");

                    b.Property<byte[]>("FotoFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titolo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("titolo");

                    b.Property<bool>("Visibilità")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("foto");
                });

            modelBuilder.Entity("CategoriaFoto", b =>
                {
                    b.HasOne("net_il_mio_fotoalbum.Models.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("net_il_mio_fotoalbum.Models.Foto", null)
                        .WithMany()
                        .HasForeignKey("FotosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
