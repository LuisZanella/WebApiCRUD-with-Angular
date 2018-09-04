   var uri = 'api/User';

        $(document).ready(function () {
        // Send an AJAX request
        $.getJSON(uri)
            .done(function (data) {
                // On success, 'data' contains a list of your class.
                $.each(data, function (key, item) {
                    // Add a list for the items.
                    $('<li>', { text: formatItem(item) }).appendTo($('#tblUsuarios'));
                });
            });
         });

        function formatItem(User) {
            return User.Id + ' | ' + User.Name + ' | '+ User.FLastName + ' | ' + User.SLastName + ' | ' + User.Nick + ' | ' + User.Password + ' | ' + User.BirthDate;
        }

        function find() {
            var id = $('#txtId').val();
    $.getJSON(uri + '/' + id)
                .done(function (data) {
        $('#User').text(formatItem(data));
        })
                .fail(function (jqXHR, textStatus, err) {
        $('#User').text('Error: ' + err);
                 });
        }