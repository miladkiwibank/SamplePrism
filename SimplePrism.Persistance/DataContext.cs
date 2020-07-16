using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Persistance
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Name=DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            #region 示例

            //设置WarehouseConsumption双主键
            //modelBuilder.Entity<WarehouseConsumption>().HasKey((WarehouseConsumption p) => new
            //{
            //    p.Id,
            //    p.PeriodicConsumptionId
            //});
            //设置WarehouseConsumption字段名Id为自增列
            //modelBuilder.Entity<WarehouseConsumption>().Property<int>((WarehouseConsumption p) => p.Id).HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            //设置PeriodicConsumption外键
            //modelBuilder.Entity<PeriodicConsumption>().HasMany<WarehouseConsumption>((PeriodicConsumption p) => p.WarehouseConsumptions).WithRequired().HasForeignKey<int>((WarehouseConsumption x) => x.PeriodicConsumptionId);
            //modelBuilder.Entity<PeriodicConsumptionItem>().HasKey((PeriodicConsumptionItem p) => new
            //{
            //    p.Id,
            //    p.WarehouseConsumptionId,
            //    p.PeriodicConsumptionId
            //});
            //modelBuilder.Entity<PeriodicConsumptionItem>().Property<int>((PeriodicConsumptionItem p) => p.Id).HasDatabaseGeneratedOption(new DatabaseGeneratedOption?(DatabaseGeneratedOption.Identity));
            //设置WarehouseConsumption的外键WarehouseConsumption字段Id,PeriodicConsumptionId对应PeriodicConsumptionItems字段WarehouseConsumptionId，PeriodicConsumptionId
            //modelBuilder.Entity<WarehouseConsumption>().HasMany<PeriodicConsumptionItem>((WarehouseConsumption p) => p.PeriodicConsumptionItems).WithRequired().HasForeignKey((PeriodicConsumptionItem x) => new
            //{
            //    x.WarehouseConsumptionId,
            //    x.PeriodicConsumptionId
            //});
            //modelBuilder.Entity<Numerator>().Property((Numerator x) => x.LastUpdateTime).IsConcurrencyToken().HasColumnType("timestamp");

            #endregion 示例

            //base.OnModelCreating(modelBuilder);
            //想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//删除一对多级联删除
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();//删除多对多级联删除
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//删除复数表名契约

            //自动注册EntityTypeConfiguration
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic instance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(instance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
