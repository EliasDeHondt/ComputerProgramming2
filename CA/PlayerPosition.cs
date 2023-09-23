/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Enum PlayerPosition
namespace CA;

public enum PlayerPosition : byte
{
    Member = 1,         // Een lid van de club
    Guest = 2,          // Een gastspeler op een PadelCourt
    Instructor = 3,     // Een instructeur op een PadelCourt
    TournamentPlayer = 4 // Een toernooispeler op een PadelCourt
}