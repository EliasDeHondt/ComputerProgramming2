/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// JS code

function fetchPlayer(courtNumber) {
    fetch('/api/players?courtNumber='+courtNumber)
        .then(response => response.json())
        .then(data => {
            const responseTableBody = document.getElementById('responseTableBody');
            
            responseTableBody.innerHTML = '';
            
            data.forEach(player => {
                const row = document.createElement('tr');
                
                row.innerHTML = `
                    <td>${player.playerNumber}</td>
                    <td>${player.firstName}</td>
                    <td>${player.lastName}</td>
                    <td>${formatDate(player.birthDate)}</td>
                    <td>${player.level}</td>
                    <td>${getEnumText(player.position)}</td>
                `;

                responseTableBody.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}

function formatDate(dateString) {
    if (!dateString) return 'Onbekend';

    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
}

function getEnumText(position) { // Didn't find a better solution :/
    switch (position) {
        case 1:
            return 'Member';
        case 2:
            return 'Guest';
        case 3:
            return 'Instructor';
        case 4:
            return 'TournamentPlayer';
        default:
            return position;
    }
}

document.addEventListener("DOMContentLoaded", function() {
    const courtNumberElement = document.getElementById('courtNumber');
    const courtNumber = courtNumberElement.dataset.courtNumber

    fetchPlayer(courtNumber);
});