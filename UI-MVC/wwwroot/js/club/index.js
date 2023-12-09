/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// JS code

function fetchClubs() {
    fetch('/api/club') // Fetch data from the API endpoint
        .then(response => response.json()) // Parse the JSON response
        .then(data => {
            const responseTableBody = document.getElementById('responseTableBody');

            // Clear existing table data
            responseTableBody.innerHTML = '';

            // Loop through the data and populate the table
            data.forEach(club => {
                const row = document.createElement('tr');

                // Add club data to the table row
                row.innerHTML = `
                    <td>${club.clubNumber}</td>
                    <td>${club.name}</td>
                    <td>${club.numberOfCourts}</td>
                    <td>${club.streetName}</td>
                    <td>${club.houseNumber}</td>
                    <td>${club.zipCode}</td>
                `;

                responseTableBody.appendChild(row); // Append row to the table body
            });
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}

document.getElementById('reloadButton').addEventListener('click', fetchClubs);
document.addEventListener("DOMContentLoaded", fetchClubs);