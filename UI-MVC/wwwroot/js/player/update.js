/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// JS code

document.addEventListener("DOMContentLoaded", function() {
    const updateLevelForm = document.getElementById('updateLevelForm');

    if (updateLevelForm) {
        updateLevelForm.addEventListener('submit', function(event) {
            event.preventDefault();

            const playerNumber = document.getElementById('playerNumber').value;
            const newLevel = document.getElementById('newLevel').value;

            console.log(playerNumber);
            console.log(newLevel);

            fetch('/api/updatePlayerLevel/' + playerNumber + '/' + newLevel, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
            })
                .then(response => {
                    if (response.ok) {
                        console.log('Player level updated successfully');
                    }
                })
                .catch(error => {
                    console.error('An error has occurred:', error);
                });
        });
    }
});