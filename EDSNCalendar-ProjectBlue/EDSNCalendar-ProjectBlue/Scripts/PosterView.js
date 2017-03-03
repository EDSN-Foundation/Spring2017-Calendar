

$(document).ready(function () {
  'use strict';
  //Setup Button
  let button = document.createElement('button');
  button.className = 'fc-posterView-button fc-button fc-state-default fc-corner-right';
  button.innerHTML = "poster"
  //Setup Listener
  button.addEventListener('click', function () {
    if (!/fc-state-active/ig.test(button.className)) {
      button.className += ' fc-state-active'
      $(".fc-view-container")[0].style.display = 'none'
      $(".poster")[0].style.display = 'block';
    } else {
      button.className = button.className.replace(/fc-state-active/ig, "");
      $(".fc-view-container")[0].style.display = 'block'
      $(".poster")[0].style.display = 'none';
    }
  });
  //Add button to calendar
  $(".fc-button-group")[1].append(button);

  //Extract Data From HTML Event and Render Data On Page In a Hidden Dev
  let data = events;
  data.forEach((event) => {
    $(".poster-events")[0].append(
    //console.log($(`<div class="col-xs-3"></div> `).text("Test")[0])
    $(
    `<div class="poster-event col-xs-3">
      <h3 class ="poster-event">
        ${event.title}@<span class ="poster-event">${event.venueName}</span>
      </h3>
      <br />
      <p>${event.date}</p>
      <image class="img-responsive"src="${(event.image !== null) ? 'data:image/png;base64,' + event.image : ''}"></img>
     </div>`)[0]
     )
  })
});