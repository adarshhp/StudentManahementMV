# Assignment Project

ASP.NET Core MVC student management project using PostgreSQL.

## Database Setup

The application expects a PostgreSQL database named `SchoolManagement`.

Connection string used by the project:

```json
"DefaultConnection": "Host=localhost;Port=5432;Database=SchoolManagement;Username=postgres;Password=postgres"
```

Create the database first:

```sql
CREATE DATABASE "SchoolManagement";
```

Connect to the database before running the table script:

```sql
\c SchoolManagement
```

Then run the following SQL:

```sql
-- ==========================
-- Students Table
-- ==========================
CREATE TABLE "Students"
(
    "StudentId" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,

    "StudentCode" VARCHAR(20) NOT NULL UNIQUE,

    "FirstName" VARCHAR(50) NOT NULL,

    "LastName" VARCHAR(50) NOT NULL,

    "Age" INTEGER NOT NULL,

    "DateOfBirth" DATE NOT NULL,

    "Gender" VARCHAR(20) NOT NULL,

    "Email" VARCHAR(100) NOT NULL,

    "Phone" VARCHAR(15) NOT NULL,

    "Address" VARCHAR(250),

    "Username" VARCHAR(50) NOT NULL,

    "Password" VARCHAR(100) NOT NULL,

    "ProfileImagePath" VARCHAR(250),

    "CreatedDate" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    "UpdatedDate" TIMESTAMP,

    "IsDeleted" BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE UNIQUE INDEX IX_Students_StudentCode
ON "Students"("StudentCode");

CREATE UNIQUE INDEX IX_Students_Username
ON "Students"("Username");

-- ==========================
-- Qualifications Table
-- ==========================
CREATE TABLE "Qualifications"
(
    "QualificationId" INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,

    "StudentId" INTEGER NOT NULL,

    "CourseName" VARCHAR(100) NOT NULL,

    "University" VARCHAR(100) NOT NULL,

    "PassingYear" INTEGER NOT NULL,

    "Percentage" NUMERIC(5,2) NOT NULL,

    CONSTRAINT FK_Qualifications_Students
    FOREIGN KEY ("StudentId")
    REFERENCES "Students"("StudentId")
    ON DELETE CASCADE
);

CREATE INDEX IX_Qualifications_StudentId
ON "Qualifications"("StudentId");
```

## Run The Project

Restore packages and run the application:

```bash
dotnet restore
dotnet run
```

Open the local URL shown in the terminal.
