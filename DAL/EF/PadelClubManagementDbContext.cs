/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class PadelClubManagementDbContext
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace PadelClubManagement.DAL.EF;

public class PadelClubManagementDbContext : DbContext
{
    public PadelClubManagementDbContext()
    {
        
    }
}