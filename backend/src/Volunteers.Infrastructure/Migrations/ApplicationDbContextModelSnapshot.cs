﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volunteers.Infrastructure.Contexts;

#nullable disable

namespace Volunteers.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Pet.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("CommonDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("common_description");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<int>("HelpStatus")
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<string>("HelthDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("helth_description");

                    b.Property<string>("Locations")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("locations");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("nickname");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("integer")
                        .HasColumnName("phone_number");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("photo");

                    b.Property<string>("PhysicalParameters")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("physical_parameters");

                    b.Property<string>("Requisites")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("requisites");

                    b.Property<string>("SpeciesBreed")
                        .HasColumnType("text")
                        .HasColumnName("species_breed");

                    b.Property<Guid>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<double>("ExperienceInYears")
                        .HasMaxLength(4)
                        .HasColumnType("double precision")
                        .HasColumnName("experience_in_years");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("integer")
                        .HasColumnName("phone_number");

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("Volunteers.Domain.SpeciesManagment.Breed.Entities.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.Property<Guid>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("Volunteers.Domain.SpeciesManagment.Species.AggregateRoot.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Pet.Entities.Pet", b =>
                {
                    b.HasOne("Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer", "Volunteer")
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer", b =>
                {
                    b.OwnsOne("Volunteers.Domain.PetManagment.Volunteer.ValueObjects.RequisiteDetails", "RequisiteDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("RequisiteDetails");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("Volunteers.Domain.PetManagment.Volunteer.ValueObjects.VolunteerRequisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("RequisiteDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)");

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("RequisiteDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisiteDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_requisite_details_volunteer_id");
                                });

                            b1.Navigation("Requisites");
                        });

                    b.OwnsOne("Volunteers.Domain.PetManagment.Volunteer.ValueObjects.SocialNetworkDetails", "SocialNetworkDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("SocialNetworkDetails");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("Volunteers.Domain.PetManagment.Volunteer.ValueObjects.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)");

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("SocialNetworkDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_network_details_volunteer_id");
                                });

                            b1.Navigation("SocialNetworks");
                        });

                    b.Navigation("RequisiteDetails");

                    b.Navigation("SocialNetworkDetails");
                });

            modelBuilder.Entity("Volunteers.Domain.SpeciesManagment.Breed.Entities.Breed", b =>
                {
                    b.HasOne("Volunteers.Domain.SpeciesManagment.Species.AggregateRoot.Species", "Species")
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_breeds_species_species_id");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Volunteers.Domain.SpeciesManagment.Species.AggregateRoot.Species", b =>
                {
                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}
