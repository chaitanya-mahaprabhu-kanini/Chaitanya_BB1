﻿// <auto-generated />
using Chaitanya_BB1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chaitanya_BB1.Migrations
{
    [DbContext(typeof(HotelRoomDbContext))]
    partial class HotelRoomDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Chaitanya_BB1.Models.Hotel", b =>
                {
                    b.Property<int>("Hid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Hid"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Hid");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Chaitanya_BB1.Models.Room", b =>
                {
                    b.Property<int>("Rid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Rid"));

                    b.Property<int>("Available")
                        .HasColumnType("int");

                    b.Property<int>("Hid")
                        .HasColumnType("int");

                    b.HasKey("Rid");

                    b.HasIndex("Hid");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Chaitanya_BB1.Models.Room", b =>
                {
                    b.HasOne("Chaitanya_BB1.Models.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("Hid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Chaitanya_BB1.Models.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
