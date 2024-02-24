/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// JS code

document.addEventListener("DOMContentLoaded", function() {
    document.getElementById('updateLevelForm').addEventListener('submit', function(event) {
        event.preventDefault();

        const playerNumber = document.getElementById('playerNumber').value;
        const newLevel = document.getElementById('newLevel').value;

        fetch('/api/updatePlayerLevel/'+playerNumber+'/'+newLevel, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
        })
    });
});