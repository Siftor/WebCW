$(document).ready(function () {

    $.ajax({
        url: 'Anns/BuildAnnsTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    });

});