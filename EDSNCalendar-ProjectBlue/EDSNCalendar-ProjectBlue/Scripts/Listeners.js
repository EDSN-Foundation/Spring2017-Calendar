//Add Listeners for Event Details
function handler1(event) {
  element = event.currentTarget;
  propogateDetails(element.lastChild);
  $("#DetailsPopUp")[0].style.display = 'block';
}

function handler2(event) {
  element = event.currentTarget;
  propogateDetails(element.childNodes[1].textContent.trim().replace(/@.*/ig, ""));
  $("#DetailsPopUp")[0].style.display = 'block';
}

function handler3(event) {
  element = event.currentTarget;
  element.parentNode.parentNode.style.display = 'none';
}

$(document).ready(setInterval(() => {

  Array.prototype.forEach.call($(".fc-day-grid-event"), (element) => {
    element.addEventListener("click", handler1);
  })

  Array.prototype.forEach.call($(".poster-event.col-xs-3"), (element) => {
    element.addEventListener("click", handler2);
  })

  Array.prototype.forEach.call($(".close-btn"), (element) => {
    element.addEventListener("click", handler3);
  })
}, 1000))

//Propogate Details 
function propogateDetails(eventName) {
  'use strict';
  let data = JSON.parse($(".poster")[0].dataset.events);
  let eventData = data.reduce((finalData, element) => {
    if (new RegExp(String(eventName), "ig").test(element.title) || eventName === element.title) {
      finalData = element;
      return finalData;
    }
    else {
      return finalData;
    }
  }, {})
  $("#detailsHead")[0].innerHTML = `${eventData.title}
                    <br />
                    <small id="detailsTimePlace">${eventData.date}|${eventData.venueName}</small>`
  $("#detailsTime")[0].innerHTML = `${eventData.date}`;
  $("#detailsContact")[0].innerHTML = `<i class="fa fa-user" aria-hidden="true"></i> ${eventData.hostName}
  <br />
  <i class ="fa fa-phone" aria-hidden="true"></i> ${eventData.hostPhoneNumber}`;
  $("#detailsAddr")[0].innerHTML = `${eventData.venueName} - ${eventData.address}`
  $("#detailsImage")[0].src = `${(eventData.image !== null) ? 'data:image/png;base64,' + eventData.image : ''}`
}