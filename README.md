# FlowMeter app
## SOFT-ENGINEERS2023

### How to run app localy

#### Database migration
1. Make sure you have Visual Studio 2022+ installed, as project uses .Net 7.
2. App uses Postgresql database, make sure you have Postgresql installed, and new databases can be created.
3. Configure connection string to your database in ./appsettings.json file
    (If left default, new database with the specified name will be created).
4. Open Package Manager Console, run Update-Database.
