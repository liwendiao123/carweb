using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    public class UCMSContext : DbContext
    {
        public UCMSContext():base("UCMSContext")
        {
        }
        public IDbSet<SysMenuBasis> SysMenuBasis { get; set; }
        public IDbSet<SysMenuOperate> SysMenuOperate { get; set; }
        public IDbSet<SysRoleBasis> SysRoleBasis { get; set; }
        public IDbSet<SysRoleMenu> SysRoleMenu { get; set; }
        public IDbSet<SysRoleUser> SysRoleUser { get; set; }
        public IDbSet<SysUserBasis> SysUserBasis { get; set; }
        public IDbSet<SysRoleOperate> SysRoleOperate { get; set; }
        public IDbSet<SysAreas> SysAreas { get; set; }
        public IDbSet<SysShortMessage> SysShortMessage { get; set; }
        public IDbSet<SysLoginLog> SysLoginLog { get; set; }
        public IDbSet<CarBrand> CarBrand { get; set; }
        public IDbSet<CarSeries> CarSeries { get; set; }
        public IDbSet<CarType> CarType { get; set; }
        public IDbSet<SysNotice> SysNotice { get; set; }
        public IDbSet<CarInfo> CarInfo { get; set; }
        public IDbSet<CarPhoto> CarPhoto { get; set; }
        public IDbSet<SysSetting> SysSetting { get; set; }
        public IDbSet<SysFile> SysFile { get; set; }
        public IDbSet<SysHotSearch> SysHotSearch { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //指定单数形式的表名 默认生成复数
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //id不自增标识 在实体属性标识也可以[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
            modelBuilder.Entity<SysMenuBasis>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysRoleBasis>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysRoleMenu>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysRoleUser>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysUserBasis>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysUserBasis>().Property(p => p.LoginCode).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<SysMenuOperate>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysRoleOperate>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<CarBrand>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<CarSeries>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<CarType>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysNotice>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<CarInfo>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<CarPhoto>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysSetting>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<SysFile>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
        }
    }
}
