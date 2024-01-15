$(document).ready(function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        locale: "pt-br",
        themeSystem: "bootstrap5",
        height: 800,
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        buttonText: {
            today: 'Hoje',
            month: 'Mês',
            week: 'Semana',
            day: 'Dia',
            list: 'Lista'
        },
        navLinks: true, // can click day/week names to navigate views
        selectable: true,
        selectMirror: true,
        select: function (arg) {
            var title = prompt('Event Title:');
            if (title) {
                calendar.addEvent({
                    title: title,
                    start: arg.start,
                    end: arg.end,
                    allDay: arg.allDay
                })
            }
            calendar.unselect()
        },
        eventClick: function (arg) {
            if (confirm('Are you sure you want to delete this event?')) {
                arg.event.remove()
            }
        },
        editable: true,
        dayMaxEvents: true, // allow "more" link when too many events
        events: [
            {
                title: 'All Day Event',
                start: '2024-01-01'
            },
            {
                title: 'Long Event',
                start: '2024-01-07',
                end: '2024-01-10'
            },
            {
                groupId: 999,
                title: 'Repeating Event',
                start: '2024-01-09T16:00:00'
            },
            {
                groupId: 999,
                title: 'Repeating Event',
                start: '2024-01-16T16:00:00'
            },
            {
                title: 'Conference',
                start: '2024-01-11',
                end: '2024-01-13'
            },
        ]
    });

    calendar.render();
});
