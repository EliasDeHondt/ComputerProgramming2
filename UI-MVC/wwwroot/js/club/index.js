/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// JS code

function fetchClubs() {
    fetch('/api/club')
        .then(response => response.json())
        .then(data => {
            const responseTableBody = document.getElementById('responseTableBody');
            
            responseTableBody.innerHTML = '';
            
            data.forEach(club => {
                const row = document.createElement('tr');
                
                row.innerHTML = `
                    <td>${club.clubNumber}</td>
                    <td>${club.name}</td>
                    <td>${club.numberOfCourts}</td>
                    <td>${club.streetName}</td>
                    <td>${club.houseNumber}</td>
                    <td>${club.zipCode}</td>
                `;

                responseTableBody.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}

document.getElementById('reloadButton').addEventListener('click', fetchClubs);
document.addEventListener("DOMContentLoaded", fetchClubs);