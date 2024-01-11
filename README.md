# Computer Programming 2

* Name: Elias De Hondt
* Student number: 0160712-80
* Academic year: 2023-2024
* Class group: ISB204B
* Subject: Padel Club Management
* Club 1-* PadelCourt 1-* Booking *-1 Player


#### Project [Computer Programming 2] Second year exercises of training applied computer science at KdG.

## Sprint 6

### Request
```http request
###
// Request to get all Clubs (1)
GET https://localhost:7074/api/Clubs HTTP/1.1
###
###
// Request to add a new Club (2)
POST https://localhost:7074/api/Clubs HTTP/1.1
Content-Type: application/json

{
    "name": "Padel club",
    "numberOfCourts": 2,
    "streetName": "Kerkstraat",
    "houseNumber": 1,
    "zipCode": "9000"
}
###
###
// Request to get a player by id (3)
GET https://localhost:7074/api/player/1 HTTP/1.1
###
###
// Request to get all Players (4)
GET https://localhost:7074/api/players HTTP/1.1
###
###
// Request to add a PadelCourt to a Player (5)
POST https://localhost:7074/api/addPadelCourtToPlayer/1/3/bookings HTTP/1.1
Content-Type: application/json

{
  "BookingDate": "2023-12-15",
  "StartTime": "10:00:00",
  "EndTime": "12:00:00"
}
###
```

### Response
```text
// Request to get all Clubs (1)
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Date: Fri, 22 Dec 2023 14:19:07 GMT
Server: Kestrel
Transfer-Encoding: chunked

// Request to add a new Club (2)
HTTP/1.1 200 OK
Content-Length: 0
Date: Fri, 22 Dec 2023 14:21:28 GMT
Server: Kestrel

// Request to get a player by id (3)
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Date: Fri, 22 Dec 2023 14:21:42 GMT
Server: Kestrel
Transfer-Encoding: chunked

// Request to get all Players (4)
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Date: Fri, 22 Dec 2023 14:22:37 GMT
Server: Kestrel
Transfer-Encoding: chunked

// Request to add a PadelCourt to a Player (5)
HTTP/1.1 201 Created
Content-Length: 0
Date: Fri, 22 Dec 2023 14:22:10 GMT
Server: Kestrel
Location: https://localhost:7074/api/player/1
```

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

### Both search criteria completed
```sql
SELECT "p"."CourtNumber", "p"."Capacity", "p"."ClubNumber", "p"."IsIndoor", "p"."PlayerNumber", "p"."Price"
FROM "PadelCourts" AS "p"
WHERE (@__price_0 IS NULL OR "p"."Price" = @__price_0) AND (@__indoor_1 IS NULL OR "p"."IsIndoor" = @__indoor_1)
```

### Search only by price
```sql
SELECT "p"."CourtNumber", "p"."Capacity", "p"."ClubNumber", "p"."IsIndoor", "p"."PlayerNumber", "p"."Price"
FROM "PadelCourts" AS "p"
WHERE @__price_0 IS NULL OR "p"."Price" = @__price_0
```

### Only search by indoor
```sql
SELECT "p"."CourtNumber", "p"."Capacity", "p"."ClubNumber", "p"."IsIndoor", "p"."PlayerNumber", "p"."Price"
FROM "PadelCourts" AS "p"
WHERE @__indoor_0 IS NULL OR "p"."IsIndoor" = @__indoor_0
```

### Both search criteria empty
```sql
SELECT "p"."CourtNumber", "p"."Capacity", "p"."ClubNumber", "p"."IsIndoor", "p"."PlayerNumber", "p"."Price"
FROM "PadelCourts" AS "p"
```