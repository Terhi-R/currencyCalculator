# currencyCalculator

App:
- Console application: currency exchange can be asked and given in the console
- AppDatabaseContext & Migrations: With Entity Framework, a table to a SQL database (based in Azure) was created 
- SeedData: tables in the database were seeded with the data found in the existing local csv file
- User-secrets: Connection string hidden with user-secrets

Business:
- RatesHandler: reading rates from a csv file
- Calculator
    - calculating currency excange and taking data from local csv file
    - optional input: Date. If user puts in date, data is taken from external api

Worker:
- With the help of Coravel, a scheduler posts latest rates to the database

Tests:
- Unit tests for the Calculator and external Api