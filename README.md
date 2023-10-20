# Project .NET Fundamentals & Extended

* Naam: Elias De Hondt
* Studentennummer: 0160712-80
* Academiejaar: 2023-2024
* Klasgroep: ISB204B
* Onderwerp: Padel Club Management -> Club 1-* PadelCourt \*-\* Player


#### Rider Projects [.NET Fundamentals-Extended] Second year exercises of training applied computer science at the KdG.

## Sprint 4

```mermaid
classDiagram
    class Club {
        ClubNumber: int
        PadelCourts: PadelCourt[]
        Name: string
        NumberOfCours: int
        StreetName: string
        HouseNumber: int
        ZipCode: int

    }

    class PadelCourt {
        CourtNumber: int
        Bookings: Booking[]
        Club: Club
        IsIndoor: bool
        Capacity: int
        Price: double
    }

    class Booking {
        BookingNumber: int
        Player: Player
        PadelCourt: PadelCourt
        BookingDate: DateOnly
        StartTime: TimeSpan
        EndTime: TimeSpan
    }

    class Player {
        PlayerNumber: int
        Bookings: Booking[]
        FirstName: string
        LastName: string
        BirthDate: DateOnly
        Level: double
        Position: PlayerPosition

    }

    Club "1" -- "*" PadelCourt
    PadelCourt "1" -- "*" Booking
    Booking "*" -- "1" Player
```

## Sprint 3

### Beide zoekcriteria ingevuld
```sql
SELECT "p"."CourtNumber", "p"."Capacity", "p"."ClubNumber", "p"."IsIndoor", "p"."PlayerNumber", "p"."Price"
FROM "PadelCourts" AS "p"
WHERE (@__price_0 IS NULL OR "p"."Price" = @__price_0) AND (@__indoor_1 IS NULL OR "p"."IsIndoor" = @__indoor_1)
```

### Enkel zoeken op price
```sql
SELECT "p"."CourtNumber", "p"."Capacity", "p"."ClubNumber", "p"."IsIndoor", "p"."PlayerNumber", "p"."Price"
FROM "PadelCourts" AS "p"
WHERE @__price_0 IS NULL OR "p"."Price" = @__price_0
```

### Enkel zoeken op indoor
```sql
SELECT "p"."CourtNumber", "p"."Capacity", "p"."ClubNumber", "p"."IsIndoor", "p"."PlayerNumber", "p"."Price"
FROM "PadelCourts" AS "p"
WHERE @__indoor_0 IS NULL OR "p"."IsIndoor" = @__indoor_0
```

### Beide zoekcriteria leeg
```sql
SELECT "p"."CourtNumber", "p"."Capacity", "p"."ClubNumber", "p"."IsIndoor", "p"."PlayerNumber", "p"."Price"
FROM "PadelCourts" AS "p"
```