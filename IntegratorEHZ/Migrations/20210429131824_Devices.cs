using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IntegratorEHZ.Migrations
{
    public partial class Devices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IMEI = table.Column<string>(type: "text", nullable: false),
                    Internal_Protocol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogSKZ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_device = table.Column<int>(type: "integer", nullable: false),
                    TimeReceivedData = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PowerLineVoltage1 = table.Column<int>(type: "integer", nullable: true),
                    EnergyMeterValue1 = table.Column<long>(type: "bigint", nullable: true),
                    PowerLineVoltage2 = table.Column<int>(type: "integer", nullable: true),
                    EnergyMeterValue2 = table.Column<long>(type: "bigint", nullable: true),
                    WorkTime = table.Column<long>(type: "bigint", nullable: true),
                    ProtectionTime = table.Column<long>(type: "bigint", nullable: true),
                    OutputCurrent = table.Column<int>(type: "integer", nullable: true),
                    OutputVoltage = table.Column<int>(type: "integer", nullable: true),
                    SumProtectPotent = table.Column<short>(type: "smallint", nullable: true),
                    PolProtectPotent = table.Column<short>(type: "smallint", nullable: true),
                    ControlMode = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState1 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState2 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState3 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState4 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState5 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState6 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState7 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState8 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState9 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState10 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState11 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState12 = table.Column<int>(type: "integer", nullable: true),
                    UnauthorisedAccess = table.Column<bool>(type: "boolean", nullable: false),
                    StationControllMode = table.Column<bool>(type: "boolean", nullable: false),
                    StationFailure = table.Column<bool>(type: "boolean", nullable: false),
                    LineDisconnection = table.Column<bool>(type: "boolean", nullable: false),
                    MainOrReserved = table.Column<bool>(type: "boolean", nullable: false),
                    DistantPowerControl = table.Column<bool>(type: "boolean", nullable: false),
                    SetOutputCurrent = table.Column<int>(type: "integer", nullable: true),
                    SetSumPotential = table.Column<short>(type: "smallint", nullable: true),
                    SetPolPotential = table.Column<short>(type: "smallint", nullable: true),
                    SetStabControlMode = table.Column<int>(type: "integer", nullable: true),
                    Errors = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogSKZ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSKZ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    deviceId = table.Column<int>(type: "integer", nullable: false),
                    PowerLineVoltage1 = table.Column<int>(type: "integer", nullable: true),
                    EnergyMeterValue1 = table.Column<long>(type: "bigint", nullable: true),
                    PowerLineVoltage2 = table.Column<int>(type: "integer", nullable: true),
                    EnergyMeterValue2 = table.Column<long>(type: "bigint", nullable: true),
                    WorkTime = table.Column<long>(type: "bigint", nullable: true),
                    ProtectionTime = table.Column<long>(type: "bigint", nullable: true),
                    OutputCurrent = table.Column<int>(type: "integer", nullable: true),
                    OutputVoltage = table.Column<int>(type: "integer", nullable: true),
                    SumProtectPotent = table.Column<short>(type: "smallint", nullable: true),
                    PolProtectPotent = table.Column<short>(type: "smallint", nullable: true),
                    ControlMode = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState1 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState2 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState3 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState4 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState5 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState6 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState7 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState8 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState9 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState10 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState11 = table.Column<int>(type: "integer", nullable: true),
                    PowerModuleState12 = table.Column<int>(type: "integer", nullable: true),
                    UnauthorisedAccess = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    StationControllMode = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    StationFailure = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LineDisconnection = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    MainOrReserved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DistantPowerControl = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    SetOutputCurrent = table.Column<int>(type: "integer", nullable: true),
                    SetSumPotential = table.Column<short>(type: "smallint", nullable: true),
                    SetPolPotential = table.Column<short>(type: "smallint", nullable: true),
                    SetStabControlMode = table.Column<int>(type: "integer", nullable: true),
                    Errors = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSKZ", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSKZ_Devices_deviceId",
                        column: x => x.deviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSKZ_deviceId",
                table: "DataSKZ",
                column: "deviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_IMEI",
                table: "Devices",
                column: "IMEI",
                unique: true);

            migrationBuilder.Sql("CREATE OR REPLACE function trigger_on_device() \n" +
               "returns trigger as $$ \n" +
               "begin \n" +
               "if (TG_OP = 'INSERT') then \n" +
               "INSERT INTO \"DataSKZ\"(\"deviceId\") VALUES(NEW.\"Id\"); \n" +
               "return NEW; \n" +
               "elseif(TG_OP = 'DELETE') then \n" +
               "DELETE FROM \"DataSKZ\" Where \"deviceId\" = OLD.\"Id\"; \n" +
               "return OLD; \n" +
               "ELSEIF(TG_OP = 'UPDATE') THEN \n" +
               "INSERT INTO \"LogSKZ\"( \n" +
               "\"Id_device\", \"TimeReceivedData\", \"PowerLineVoltage1\", \"EnergyMeterValue1\", \"PowerLineVoltage2\", \"EnergyMeterValue2\", \"WorkTime\", \"ProtectionTime\", \"OutputCurrent\", \"OutputVoltage\", \"SumProtectPotent\", \"PolProtectPotent\", \"ControlMode\", \"PowerModuleState1\", \"PowerModuleState2\", \"PowerModuleState3\", \"PowerModuleState4\", \"PowerModuleState5\", \"PowerModuleState6\", \"PowerModuleState7\", \"PowerModuleState8\", \"PowerModuleState9\", \"PowerModuleState10\", \"PowerModuleState11\", \"PowerModuleState12\", \"UnauthorisedAccess\", \"StationControllMode\", \"StationFailure\", \"LineDisconnection\", \"MainOrReserved\", \"DistantPowerControl\", \"SetOutputCurrent\", \"SetSumPotential\", \"SetPolPotential\", \"SetStabControlMode\", \"Errors\") \n" +
               "VALUES(NEW.\"deviceId\", now(), NEW.\"PowerLineVoltage1\", NEW.\"EnergyMeterValue1\", NEW.\"PowerLineVoltage2\", NEW.\"EnergyMeterValue2\", NEW.\"WorkTime\", NEW.\"ProtectionTime\", NEW.\"OutputCurrent\", NEW.\"OutputVoltage\", NEW.\"SumProtectPotent\", NEW.\"PolProtectPotent\", NEW.\"ControlMode\", NEW.\"PowerModuleState1\", NEW.\"PowerModuleState2\", NEW.\"PowerModuleState3\", NEW.\"PowerModuleState4\", NEW.\"PowerModuleState5\", NEW.\"PowerModuleState6\", NEW.\"PowerModuleState7\", NEW.\"PowerModuleState8\", NEW.\"PowerModuleState9\", NEW.\"PowerModuleState10\", NEW.\"PowerModuleState11\", NEW.\"PowerModuleState12\", NEW.\"UnauthorisedAccess\", NEW.\"StationControllMode\", NEW.\"StationFailure\", NEW.\"LineDisconnection\", NEW.\"MainOrReserved\", NEW.\"DistantPowerControl\", NEW.\"SetOutputCurrent\", NEW.\"SetSumPotential\", NEW.\"SetPolPotential\", NEW.\"SetStabControlMode\", NEW.\"Errors\"); \n" +
               "RETURN NEW; \n" +
               "end if; \n" +
               "end; $$ \n" +
               "language plpgsql; \n");

            migrationBuilder.Sql("CREATE TRIGGER trigger_on_device \n" +
                "AFTER INSERT OR DELETE \n" +
                "ON \"Devices\" \n" +
                "FOR EACH ROW \n" +
                "EXECUTE PROCEDURE trigger_on_device(); \n");

            migrationBuilder.Sql("CREATE TRIGGER trigger_on_device \n" +
                "AFTER UPDATE \n" +
                "ON \"DataSKZ\" \n" +
                "FOR EACH ROW \n" +
                "EXECUTE PROCEDURE trigger_on_device(); \n");


            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION NotifiData() RETURNS TRIGGER AS $$ \n" +
                "BEGIN \n" +
                "IF TG_OP = 'INSERT' then PERFORM pg_notify('notifydataskz', 'new record inserted'); \n" +
                "ELSIF TG_OP = 'UPDATE' then PERFORM pg_notify('notifydataskz', 'updated'); \n" +
                "ELSIF TG_OP = 'DELETE' then PERFORM pg_notify('notifydataskz', 'deleted'); \n" +
                "END IF; RETURN NULL; \n" +
                "END; $$ LANGUAGE plpgsql; \n");

            migrationBuilder.Sql("CREATE TRIGGER any_after_DataSKZ \n" +
                "AFTER INSERT OR DELETE OR UPDATE \n" +
                "ON \"DataSKZ\" \n" +
                "FOR EACH ROW \n" +
                "EXECUTE PROCEDURE NotifiData(); \n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSKZ");

            migrationBuilder.DropTable(
                name: "LogSKZ");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
