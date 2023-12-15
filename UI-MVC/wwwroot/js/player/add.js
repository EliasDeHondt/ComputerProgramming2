/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// JS code

function fetchPlayersfromPadelCourtSelect() {
    const playerSelect = document.getElementById('playerSelect');
    playerSelect.innerHTML = "";
    
    fetch('/api/players')
        .then(response => response.json())
        .then(data => {
            data.forEach(player => {
                const option = document.createElement('option');
                option.value = player.playerNumber;
                option.text = `${player.firstName} ${player.lastName}`;
                playerSelect.appendChild(option);
            });
        })
        .catch(error => {
            console.error('Error fetching players from Padel Court:', error);
        });
}

function addPadelCourtToPlayer(playerNumber, courtNumber, bookingDate, startTime, endTime) {
    fetch('/api/addPadelCourtToPlayer/'+courtNumber+'/'+playerNumber+'/'+bookingDate+'/'+startTime+'/'+endTime, {
        method: 'POST'
    })
        .then(response => {
            fetchPlayersfromPadelCourtSelect();
            fetchPlayersFromPadelCourt(courtNumber);
        })
        .catch(error => {
            console.error('Er is een fout opgetreden:', error);
        });
}

document.addEventListener("DOMContentLoaded", function() {
    fetchPlayersfromPadelCourtSelect();

    const addPlayerButton = document.getElementById('addPlayerButton');
    addPlayerButton.addEventListener('click', function(event) {
        event.preventDefault();

        const playerSelect = document.getElementById('playerSelect');
        const playerNumber = playerSelect.value;

        const startTimeInput = document.getElementById('startTimeInput');
        const endTimeInput = document.getElementById('endTimeInput');

        const startTime = startTimeInput.value;
        const endTime = endTimeInput.value;

        const bookingDate = new Date().toISOString().slice(0, 10);

        const courtNumberElement = document.getElementById('courtNumber');
        const courtNumber = courtNumberElement.dataset.courtNumber;
        
        addPadelCourtToPlayer(playerNumber, courtNumber, bookingDate, startTime, endTime);
    });
});