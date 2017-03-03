$(document).ready(function () {
    if ($('.calendar').length) {
        events = JSON.parse($('.calendar').attr('data-events'));
        $('.calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,listMonth,posterView'
            },
            defaultDate: '2016-12-13',
            editable: false,
            events: events
        });
    }
    $(".poster")[0].style.display = 'none';

    $('.Categories .chosen-drop').click(function () {
        eventProperties.categories = getProperties($('.Categories .chosen-choices'));
        updateEvents();
    });
    $('.Tags .chosen-drop').click(function () {
        eventProperties.tags = getProperties($('.Tags .chosen-choices'));
        updateEvents();
    });
    $('.Region .chosen-drop').click(function () {
        eventProperties.region = getProperties($('.Region .chosen-choices'));
        updateEvents();
    });
    $('.Country .chosen-drop').click(function () {
        eventProperties.country = getProperties($('.Country .chosen-choices'));
        updateEvents();
    });
    $('.Programs .chosen-drop').click(function () {
        eventProperties.programs = getProperties($('.Programs .chosen-choices'));
        updateEvents();
    });

    $('.Categories .chosen-choices').mouseup(function () {
        setTimeout(function () {
            eventProperties.categories = getProperties($('.Categories .chosen-choices'));
            updateEvents();
        }, 20);
    });
    $('.Tags .chosen-choices').mouseup(function () {
        setTimeout(function () {
            eventProperties.tags = getProperties($('.Tags .chosen-choices'));
            updateEvents();
        }, 20);
    });
    $('.Region .chosen-choices').mouseup(function () {
        setTimeout(function () {
            eventProperties.region = getProperties($('.Region .chosen-choices'));
            updateEvents();
        }, 20);
    });
    $('.Country .chosen-choices').mouseup(function () {
        setTimeout(function () {
            eventProperties.country = getProperties($('.Country .chosen-choices'));
            updateEvents();
        }, 20);
    });
    $('.Programs .chosen-choices').mouseup(function () {
        setTimeout(function () {
            eventProperties.programs = getProperties($('.Programs .chosen-choices'));
            updateEvents();
        }, 20);
    });

});

function getProperties(selectedItems) {
    var children = selectedItems[0].children;
    var properties = [];
    for (var i = 0; i < children.length - 1; i++) {
        if (children[i].classList.contains('search-choice')) {
            var filter = $(children[i].children[0]).text();
            properties.push(filter);
        }
    }
    return properties;
}

function updateEvents() {
    $('.calendar').fullCalendar('removeEventSource', events)
    if (!eventProperties.categories.length &&
            !eventProperties.tags.length &&
            !eventProperties.region.length &&
            !eventProperties.country.length &&
            !eventProperties.programs.length) {
        events = JSON.parse($('.calendar').attr('data-events'));
    } else {
        var allEvents = JSON.parse($('.calendar').attr('data-events'));
        events = [];
        var properties = JSON.parse($('.calendar').attr('data-properties'));
        var eventIds = [];
        for (var property in properties) {
            for (var e in properties[property].events) {
                if (!containsProperty(properties[property].property))
                    continue;
                var id = properties[property].events[e];
                if ($.inArray(id, eventIds) == -1) {
                    eventIds.push(id);
                }
            }
        }
        for (var e in allEvents) {
            var event = allEvents[e];
            if ($.inArray(event.id, eventIds) > -1)
                events.push(event);
        }
    }
    $('.calendar').fullCalendar('addEventSource', events)
    $('.calendar').fullCalendar('rerenderEvents');
    var myNode = document.getElementsByClassName("poster-events")[0];
    if (myNode != null && myNode != undefined)
        myNode.innerHTML = '';
    events.forEach((event) => {
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
}

function containsProperty(property) {
    for (var p in eventProperties) {
        if ($.inArray(property, eventProperties[p]) > -1)
            return true;
    }
    return false;
}

var eventProperties = {
    'categories': [],
    'tags': [],
    'region': [],
    'country': [],
    'programs': [],
}

events = [];