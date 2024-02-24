/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// JS code

function fetchClubs() {
    fetch('/api/clubs')
        .then(response => response.json())
        .then(data => {
            const responseTableBody = document.getElementById('responseTableBody');
            
            responseTableBody.innerHTML = '';
            
            data.forEach(club => {
                const row = document.createElement('tr');

                row.innerHTML = `
                    <td>${club.clubNumber != null ? club.clubNumber : 'N/A'}</td>
                    <td>${club.name != null ? club.name : 'N/A'}</td>
                    <td>${club.numberOfCourts != null ? club.numberOfCourts : 'N/A'}</td>
                    <td>${club.streetName != null ? club.streetName : 'N/A'}</td>
                    <td>${club.houseNumber != null ? club.houseNumber : 'N/A'}</td>
                    <td>${club.zipCode != null ? club.zipCode : 'N/A'}</td>
                `;

                responseTableBody.appendChild(row);
            });
        })
}

document.getElementById('reloadButton').addEventListener('click', fetchClubs);
document.addEventListener("DOMContentLoaded", fetchClubs);