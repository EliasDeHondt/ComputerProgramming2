/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Top level statements, i.e. entry point of the application (Start)
using Microsoft.EntityFrameworkCore;
using PadelClubManagement.BL;
using PadelClubManagement.DAL.EF;
using PadelClubManagement.UI.CA;

/*
InMemoryRepository repository = new InMemoryRepository();
InMemoryRepository.Seed();
Manager manager = new Manager(repository);
*/

// Composition Root
DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
PadelClubManagementDbContext padelClubManagementDbContext = new PadelClubManagementDbContext(optionsBuilder.Options);
DbContextRepository dbContextRepository = new DbContextRepository(padelClubManagementDbContext);

bool databaseCreated = padelClubManagementDbContext.CreateDatabase(true); // Create the database (Code first flow)
if (databaseCreated) DataSeeder.Seed(padelClubManagementDbContext); // Seed the database with some data

Manager manager = new Manager(dbContextRepository); // Create new instance of Manager

ConsoleUi consoleUi = new ConsoleUi(manager); // Create new instance of ConsoleUi & start the application
consoleUi.Start(); // Start the application