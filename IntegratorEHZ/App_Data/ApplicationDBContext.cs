using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IntegratorEHZ.Models;
using IntegratorEHZ.Models.SKZ;

namespace IntegratorEHZ.App_Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){}
        public DbSet<Device> Devices { get; set; }
        public DbSet<DataSKZ> DataSKZ { get; set; }
        public DbSet<LogSKZ> LogSKZ { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataSKZ>().Property(column => column.UnauthorisedAccess).HasDefaultValue(false);
            modelBuilder.Entity<DataSKZ>().Property(column => column.StationControllMode).HasDefaultValue(false);
            modelBuilder.Entity<DataSKZ>().Property(column => column.StationFailure).HasDefaultValue(false);
            modelBuilder.Entity<DataSKZ>().Property(column => column.LineDisconnection).HasDefaultValue(false);
            modelBuilder.Entity<DataSKZ>().Property(column => column.MainOrReserved).HasDefaultValue(false);
            modelBuilder.Entity<DataSKZ>().Property(column => column.DistantPowerControl).HasDefaultValue(false);
            modelBuilder.Entity<DataSKZ>().HasOne(e => e.device).WithMany(t => t.dataSKZ).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Device>().HasIndex(i => i.IMEI).IsUnique();
        }

        /*
         * !!! НЕ УДАЛЯТЬ!!!!
         Запросы на создание тригеров которые добавляются в миграцию

          migrationBuilder.Sql("CREATE OR REPLACE function trigger_on_device() " +
               "returns trigger as $$ " +
               "begin " +
               "if (TG_OP = 'INSERT') then " +
               "INSERT INTO \"DataSKZ\"(\"deviceId\") VALUES(NEW.\"Id\"); " +
               "return NEW; " +
               "elseif(TG_OP = 'DELETE') then " +
               "DELETE FROM \"DataSKZ\" Where \"deviceId\" = OLD.\"Id\"; " +
               "return OLD; " +
               "ELSEIF(TG_OP = 'UPDATE') THEN " +
               "INSERT INTO \"LogSKZ\"( " +
               "\"Id_device\", \"TimeReceivedData\", \"PowerLineVoltage1\", \"EnergyMeterValue1\", \"PowerLineVoltage2\", \"EnergyMeterValue2\", \"WorkTime\", \"ProtectionTime\", \"OutputCurrent\", \"OutputVoltage\", \"SumProtectPotent\", \"PolProtectPotent\", \"ControlMode\", \"PowerModuleState1\", \"PowerModuleState2\", \"PowerModuleState3\", \"PowerModuleState4\", \"PowerModuleState5\", \"PowerModuleState6\", \"PowerModuleState7\", \"PowerModuleState8\", \"PowerModuleState9\", \"PowerModuleState10\", \"PowerModuleState11\", \"PowerModuleState12\", \"UnauthorisedAccess\", \"StationControllMode\", \"StationFailure\", \"LineDisconnection\", \"MainOrReserved\", \"DistantPowerControl\", \"SetOutputCurrent\", \"SetSumPotential\", \"SetPolPotential\", \"SetStabControlMode\", \"Errors\") " +
               "VALUES(NEW.\"deviceId\", now(), NEW.\"PowerLineVoltage1\", NEW.\"EnergyMeterValue1\", NEW.\"PowerLineVoltage2\", NEW.\"EnergyMeterValue2\", NEW.\"WorkTime\", NEW.\"ProtectionTime\", NEW.\"OutputCurrent\", NEW.\"OutputVoltage\", NEW.\"SumProtectPotent\", NEW.\"PolProtectPotent\", NEW.\"ControlMode\", NEW.\"PowerModuleState1\", NEW.\"PowerModuleState2\", NEW.\"PowerModuleState3\", NEW.\"PowerModuleState4\", NEW.\"PowerModuleState5\", NEW.\"PowerModuleState6\", NEW.\"PowerModuleState7\", NEW.\"PowerModuleState8\", NEW.\"PowerModuleState9\", NEW.\"PowerModuleState10\", NEW.\"PowerModuleState11\", NEW.\"PowerModuleState12\", NEW.\"UnauthorisedAccess\", NEW.\"StationControllMode\", NEW.\"StationFailure\", NEW.\"LineDisconnection\", NEW.\"MainOrReserved\", NEW.\"DistantPowerControl\", NEW.\"SetOutputCurrent\", NEW.\"SetSumPotential\", NEW.\"SetPolPotential\", NEW.\"SetStabControlMode\", NEW.\"Errors\"); " +
               "RETURN NEW; " +
               "end if; " +
               "end; $$ " +
               "language plpgsql; ");

            migrationBuilder.Sql("CREATE TRIGGER trigger_on_device " +
                "AFTER INSERT OR DELETE " +
                "ON \"Devices\" " +
                "FOR EACH ROW " +
                "EXECUTE PROCEDURE trigger_on_device(); ");

            migrationBuilder.Sql("CREATE TRIGGER trigger_on_device " +
                "AFTER UPDATE " +
                "ON \"DataSKZ\" " +
                "FOR EACH ROW " +
                "EXECUTE PROCEDURE trigger_on_device(); ");
         
          
            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION NotifiData() RETURNS TRIGGER AS $$ "+
            "BEGIN " +
            "IF TG_OP = 'INSERT' then PERFORM pg_notify('notifydataskz', 'new record inserted'); " +
            "ELSIF TG_OP = 'UPDATE' then PERFORM pg_notify('notifydataskz', 'updated'); " +
            "ELSIF TG_OP = 'DELETE' then PERFORM pg_notify('notifydataskz', 'deleted'); " +
            "END IF; RETURN NULL; " +
            "END; $$ LANGUAGE plpgsql; ");
          
            migrationBuilder.Sql("CREATE TRIGGER any_after_DataSKZ " +
                "AFTER INSERT OR DELETE OR UPDATE " +
                "ON \"DataSKZ\" " +
                "FOR EACH ROW " +
                "EXECUTE PROCEDURE NotifiData(); ");
          */
    }
}
