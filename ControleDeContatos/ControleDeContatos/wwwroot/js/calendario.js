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
            $("#modalCreateEvent").modal("toggle")
        },
        eventClick: function (arg) {
            if (confirm('Are you sure you want to delete this event?')) {
                arg.event.remove()
            }
        },
        editable: true,
        dayMaxEvents: true, // allow "more" link when too many events
        events: function (info, successCallback, failureCallback)
        {
            $.ajax({
                url: '/Usuario/BuscarCompromissosUsuarioLogado', // Substitua pelo URL real
                type: 'GET',
                dataType: 'json',
                success: function (response) {
                    var events = [];
                    // Processar os eventos recebidos
                    $.each(response, function (index, eventData) {
                        events.push({
                            title: eventData.title,
                            start: eventData.data_entrega,
                            color: "#8687A6"
                            // Adicione outras propriedades do evento conforme necessário
                        });
                    });

                    // Chamar o callback de sucesso com os eventos processados
                    successCallback(events);
                },
                error: function (error) {
                    // Chamar o callback de falha em caso de erro na requisição
                    failureCallback(error);
                }
            });
        }
    });

    calendar.render();
});
