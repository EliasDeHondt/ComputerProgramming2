/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Top level statements, i.e. entry point of the application (Start)

using PadelClubManagement.BL;
using PadelClubManagement.DAL;
using PadelClubManagement.UI.CA;

InMemoryRepository repository = new InMemoryRepository(); // Create new instance of InMemoryRepository
Manager manager = new Manager(repository); // Create new instance of Manager

InMemoryRepository.Seed(); // Seed the repository with some data

ConsoleUi consoleUi = new ConsoleUi(manager); // Create new instance of ConsoleUi & start the application