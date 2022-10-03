﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pallet.DatabaseGenerated
{
    public partial class WOODNAIL_DB_PavelContext : DbContext
    {
        public WOODNAIL_DB_PavelContext()
        {
        }

        public WOODNAIL_DB_PavelContext(DbContextOptions<WOODNAIL_DB_PavelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlarmDef> AlarmDef { get; set; }
        public virtual DbSet<AlarmLog> AlarmLog { get; set; }
        public virtual DbSet<NailerDef> NailerDef { get; set; }
        public virtual DbSet<ProfileDef> ProfileDef { get; set; }
        public virtual DbSet<ProgLang> ProgLang { get; set; }
        public virtual DbSet<ProgParam> ProgParam { get; set; }
        public virtual DbSet<ProgTxt> ProgTxt { get; set; }
        public virtual DbSet<ProgUser> ProgUser { get; set; }
        public virtual DbSet<WprodDef> WprodDef { get; set; }
        public virtual DbSet<WprodElePos> WprodElePos { get; set; }
        public virtual DbSet<WprodElements> WprodElements { get; set; }
        public virtual DbSet<WprodNailPos> WprodNailPos { get; set; }
        public virtual DbSet<WtableDef> WtableDef { get; set; }
        public virtual DbSet<WtableTools> WtableTools { get; set; }
        public virtual DbSet<WtableWprod> WtableWprod { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Czech_CI_AS");

            modelBuilder.Entity<AlarmDef>(entity =>
            {
                entity.ToTable("ALARM_DEF");

                entity.HasIndex(e => e.AlmName, "IX_ALARM_DEF_1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AlmAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ALM_ADDRESS");

                entity.Property(e => e.AlmName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ALM_NAME");

                entity.Property(e => e.AlmNr).HasColumnName("ALM_NR");

                entity.Property(e => e.Device)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DEVICE");

                entity.Property(e => e.Inverted).HasColumnName("INVERTED");

                entity.Property(e => e.Prio)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PRIO")
                    .IsFixedLength();

                entity.Property(e => e.Stype)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STYPE")
                    .IsFixedLength();

                entity.Property(e => e.Text1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("TEXT1");

                entity.Property(e => e.Text2)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("TEXT2");

                entity.Property(e => e.Text3)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("TEXT3");
            });

            modelBuilder.Entity<AlarmLog>(entity =>
            {
                entity.ToTable("ALARM_LOG");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AlmAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ALM_ADDRESS");

                entity.Property(e => e.AlmName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ALM_NAME");

                entity.Property(e => e.AlmNr).HasColumnName("ALM_NR");

                entity.Property(e => e.Device)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DEVICE");

                entity.Property(e => e.Gone).HasColumnName("GONE");

                entity.Property(e => e.Inverted).HasColumnName("INVERTED");

                entity.Property(e => e.Prio)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PRIO")
                    .IsFixedLength();

                entity.Property(e => e.Stype)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STYPE")
                    .IsFixedLength();

                entity.Property(e => e.Text1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("TEXT1");

                entity.Property(e => e.Text2)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("TEXT2");

                entity.Property(e => e.Text3)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("TEXT3");

                entity.Property(e => e.Timestamp1)
                    .HasColumnType("datetime")
                    .HasColumnName("TIMESTAMP1");

                entity.Property(e => e.Timestamp2)
                    .HasColumnType("datetime")
                    .HasColumnName("TIMESTAMP2");
            });

            modelBuilder.Entity<NailerDef>(entity =>
            {
                entity.ToTable("NAILER_DEF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Desc1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("DESC1");

                entity.Property(e => e.Desc2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("DESC2");

                entity.Property(e => e.Desc3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("DESC3");

                entity.Property(e => e.Dock).HasColumnName("DOCK");

                entity.Property(e => e.Mcolor).HasColumnName("MCOLOR");

                entity.Property(e => e.Msize).HasColumnName("MSIZE");

                entity.Property(e => e.NailerId).HasColumnName("NAILER_ID");

                entity.Property(e => e.NailerName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAILER_NAME");

                entity.Property(e => e.Ncap).HasColumnName("NCAP");

                entity.Property(e => e.Nlength).HasColumnName("NLENGTH");

                entity.Property(e => e.Nwidth).HasColumnName("NWIDTH");

                entity.Property(e => e.Nwrn).HasColumnName("NWRN");
            });

            modelBuilder.Entity<ProfileDef>(entity =>
            {
                entity.ToTable("PROFILE_DEF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreaBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CREA_BY");

                entity.Property(e => e.Desc1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("DESC1");

                entity.Property(e => e.Desc2)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("DESC2");

                entity.Property(e => e.Desc3)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("DESC3");

                entity.Property(e => e.DtChng)
                    .HasColumnType("datetime")
                    .HasColumnName("DT_CHNG");

                entity.Property(e => e.DtCrea)
                    .HasColumnType("datetime")
                    .HasColumnName("DT_CREA");

                entity.Property(e => e.DtOpen)
                    .HasColumnType("datetime")
                    .HasColumnName("DT_OPEN");

                entity.Property(e => e.ProfileId).HasColumnName("PROFILE_ID");

                entity.Property(e => e.ProfileName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PROFILE_NAME");

                entity.Property(e => e.RobotId).HasColumnName("ROBOT_ID");

                entity.Property(e => e.StoreId).HasColumnName("STORE_ID");

                entity.Property(e => e.WprodId).HasColumnName("WPROD_ID");

                entity.Property(e => e.WtableId).HasColumnName("WTABLE_ID");
            });

            modelBuilder.Entity<ProgLang>(entity =>
            {
                entity.ToTable("PROG_LANG");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Defined).HasColumnName("DEFINED");

                entity.Property(e => e.Lang)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("LANG")
                    .IsFixedLength();

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("LANGUAGE");
            });

            modelBuilder.Entity<ProgParam>(entity =>
            {
                entity.HasKey(e => e.ParamName);

                entity.ToTable("PROG_PARAM");

                entity.Property(e => e.ParamName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PARAM_NAME");

                entity.Property(e => e.ParamValue)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PARAM_VALUE");
            });

            modelBuilder.Entity<ProgTxt>(entity =>
            {
                entity.ToTable("PROG_TXT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Defined).HasColumnName("DEFINED");

                entity.Property(e => e.Lng1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("LNG1");

                entity.Property(e => e.Lng2)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("LNG2");

                entity.Property(e => e.Lng3)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("LNG3");

                entity.Property(e => e.Obj)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("OBJ");

                entity.Property(e => e.Txt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("TXT");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<ProgUser>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.ToTable("PROG_USER");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.UserDesc)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("USER_DESC");

                entity.Property(e => e.UserHash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("USER_HASH")
                    .IsFixedLength();

                entity.Property(e => e.UserRole).HasColumnName("USER_ROLE");
            });

            modelBuilder.Entity<WprodDef>(entity =>
            {
                entity.ToTable("WPROD_DEF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Desc1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("DESC1");

                entity.Property(e => e.Desc2)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("DESC2");

                entity.Property(e => e.Desc3)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("DESC3");

                entity.Property(e => e.Preset).HasColumnName("PRESET");

                entity.Property(e => e.Prod).HasColumnName("PROD");

                entity.Property(e => e.Size1X).HasColumnName("SIZE1_X");

                entity.Property(e => e.Size1Y).HasColumnName("SIZE1_Y");

                entity.Property(e => e.Size1Z).HasColumnName("SIZE1_Z");

                entity.Property(e => e.Size2X).HasColumnName("SIZE2_X");

                entity.Property(e => e.Size2Y).HasColumnName("SIZE2_Y");

                entity.Property(e => e.Size2Z).HasColumnName("SIZE2_Z");

                entity.Property(e => e.Steps).HasColumnName("STEPS");

                entity.Property(e => e.Type).HasColumnName("TYPE");

                entity.Property(e => e.WprodId).HasColumnName("WPROD_ID");

                entity.Property(e => e.WprodName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("WPROD_NAME");
            });

            modelBuilder.Entity<WprodElePos>(entity =>
            {
                entity.ToTable("WPROD_ELE_POS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EleId).HasColumnName("ELE_ID");

                entity.Property(e => e.PosId).HasColumnName("POS_ID");

                entity.Property(e => e.Posx).HasColumnName("POSX");

                entity.Property(e => e.Posy).HasColumnName("POSY");

                entity.Property(e => e.Posz).HasColumnName("POSZ");

                entity.Property(e => e.Step).HasColumnName("STEP");

                entity.Property(e => e.WprodName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("WPROD_NAME");
            });

            modelBuilder.Entity<WprodElements>(entity =>
            {
                entity.ToTable("WPROD_ELEMENTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Desc1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("DESC1");

                entity.Property(e => e.Desc2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("DESC2");

                entity.Property(e => e.Desc3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("DESC3");

                entity.Property(e => e.EleCnt).HasColumnName("ELE_CNT");

                entity.Property(e => e.EleId).HasColumnName("ELE_ID");

                entity.Property(e => e.EleName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ELE_NAME");

                entity.Property(e => e.Layer).HasColumnName("LAYER");

                entity.Property(e => e.Outln).HasColumnName("OUTLN");

                entity.Property(e => e.Sizex).HasColumnName("SIZEX");

                entity.Property(e => e.Sizey).HasColumnName("SIZEY");

                entity.Property(e => e.Sizez).HasColumnName("SIZEZ");

                entity.Property(e => e.Step).HasColumnName("STEP");

                entity.Property(e => e.WoodDir).HasColumnName("WOOD_DIR");

                entity.Property(e => e.WprodName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("WPROD_NAME");
            });

            modelBuilder.Entity<WprodNailPos>(entity =>
            {
                entity.ToTable("WPROD_NAIL_POS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Alt).HasColumnName("ALT");

                entity.Property(e => e.Angle1).HasColumnName("ANGLE1");

                entity.Property(e => e.Angle2).HasColumnName("ANGLE2");

                entity.Property(e => e.MoveToNext).HasColumnName("MOVE_TO_NEXT");

                entity.Property(e => e.NailFix).HasColumnName("NAIL_FIX");

                entity.Property(e => e.NailId).HasColumnName("NAIL_ID");

                entity.Property(e => e.NailerId).HasColumnName("NAILER_ID");

                entity.Property(e => e.Posx).HasColumnName("POSX");

                entity.Property(e => e.Posy).HasColumnName("POSY");

                entity.Property(e => e.Posz).HasColumnName("POSZ");

                entity.Property(e => e.Step).HasColumnName("STEP");

                entity.Property(e => e.WprodName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("WPROD_NAME");
            });

            modelBuilder.Entity<WtableDef>(entity =>
            {
                entity.ToTable("WTABLE_DEF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.A1Conf)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("A1_CONF")
                    .IsFixedLength();

                entity.Property(e => e.A1OffsX).HasColumnName("A1_OFFS_X");

                entity.Property(e => e.A1OffsY).HasColumnName("A1_OFFS_Y");

                entity.Property(e => e.A2Conf)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("A2_CONF")
                    .IsFixedLength();

                entity.Property(e => e.A2OffsX).HasColumnName("A2_OFFS_X");

                entity.Property(e => e.A2OffsY).HasColumnName("A2_OFFS_Y");

                entity.Property(e => e.ASizeX).HasColumnName("A_SIZE_X");

                entity.Property(e => e.ASizeY).HasColumnName("A_SIZE_Y");

                entity.Property(e => e.AWaOffsX).HasColumnName("A_WA_OFFS_X");

                entity.Property(e => e.AWaOffsY).HasColumnName("A_WA_OFFS_Y");

                entity.Property(e => e.AWaSizeX).HasColumnName("A_WA_SIZE_X");

                entity.Property(e => e.AWaSizeY).HasColumnName("A_WA_SIZE_Y");

                entity.Property(e => e.B1Conf)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("B1_CONF")
                    .IsFixedLength();

                entity.Property(e => e.B1OffsX).HasColumnName("B1_OFFS_X");

                entity.Property(e => e.B1OffsY).HasColumnName("B1_OFFS_Y");

                entity.Property(e => e.B2Conf)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("B2_CONF")
                    .IsFixedLength();

                entity.Property(e => e.B2OffsX).HasColumnName("B2_OFFS_X");

                entity.Property(e => e.B2OffsY).HasColumnName("B2_OFFS_Y");

                entity.Property(e => e.BEna).HasColumnName("B_ENA");

                entity.Property(e => e.BSizeX).HasColumnName("B_SIZE_X");

                entity.Property(e => e.BSizeY).HasColumnName("B_SIZE_Y");

                entity.Property(e => e.BWaOffsX).HasColumnName("B_WA_OFFS_X");

                entity.Property(e => e.BWaOffsY).HasColumnName("B_WA_OFFS_Y");

                entity.Property(e => e.BWaSizeX).HasColumnName("B_WA_SIZE_X");

                entity.Property(e => e.BWaSizeY).HasColumnName("B_WA_SIZE_Y");

                entity.Property(e => e.WtableId).HasColumnName("WTABLE_ID");

                entity.Property(e => e.WtableName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("WTABLE_NAME");
            });

            modelBuilder.Entity<WtableTools>(entity =>
            {
                entity.ToTable("WTABLE_TOOLS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ToolGid1).HasColumnName("TOOL_GID1");

                entity.Property(e => e.ToolGid2).HasColumnName("TOOL_GID2");

                entity.Property(e => e.ToolGid3).HasColumnName("TOOL_GID3");

                entity.Property(e => e.ToolGid4).HasColumnName("TOOL_GID4");

                entity.Property(e => e.ToolNid1).HasColumnName("TOOL_NID1");

                entity.Property(e => e.ToolNid2).HasColumnName("TOOL_NID2");

                entity.Property(e => e.ToolNid3).HasColumnName("TOOL_NID3");

                entity.Property(e => e.ToolNid4).HasColumnName("TOOL_NID4");

                entity.Property(e => e.WtableId).HasColumnName("WTABLE_ID");
            });

            modelBuilder.Entity<WtableWprod>(entity =>
            {
                entity.ToTable("WTABLE_WPROD");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.WprodId1).HasColumnName("WPROD_ID1");

                entity.Property(e => e.WprodId2).HasColumnName("WPROD_ID2");

                entity.Property(e => e.WprodId3).HasColumnName("WPROD_ID3");

                entity.Property(e => e.WprodId4).HasColumnName("WPROD_ID4");

                entity.Property(e => e.WtableId).HasColumnName("WTABLE_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}