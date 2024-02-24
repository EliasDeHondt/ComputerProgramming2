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
}

function addPadelCourtToPlayer(playerNumber, courtNumber, booking) {
    fetch('/api/addPadelCourtToPlayer/'+courtNumber+'/'+playerNumber+'/bookings', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json' // Specify JSON content type
        },
        body: JSON.stringify(booking) // Convert the booking object to JSON string
    }).then(response => {
        fetchPlayersfromPadelCourtSelect();
        fetchPlayersFromPadelCourt(courtNumber);
    })
}

document.addEventListener("DOMContentLoaded", function() {
    fetchPlayersfromPadelCourtSelect();

    const addPlayerButton = document.getElementById('addPlayerButton');
    addPlayerButton.addEventListener('click', function(event) {
        event.preventDefault();

        const playerSelect = document.getElementById('playerSelect');
        const playerNumber = playerSelect.value;

        const startTimeInput = document.getElementById('startTimeInput').value;
        const endTimeInput = document.getElementById('endTimeInput').value;
        
        const bookingDate = new Date().toISOString().slice(0, 10);

        const courtNumberElement = document.getElementById('courtNumber');
        const courtNumber = courtNumberElement.dataset.courtNumber;
        
        const booking = {
            bookingDate: bookingDate,
            startTime: startTimeInput+':00',
            endTime: endTimeInput+':00'
        };
        
        addPadelCourtToPlayer(playerNumber, courtNumber, booking);
    });
});