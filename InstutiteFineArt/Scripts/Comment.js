var id = $('#PostId').val();
$(document).ready(function () {

    _getAllComment(id);

});


function _getAllComment(id) {
    $.ajax({
        url: "/Comment/List/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            console.log(result);
            $.each(result, function (key, item) {
                html += '<div class="media">';
                html += '<a href="#" class="pull-left">'
                html += '<img alt="Comments" style="width:67px !important;height:64px !important;" src="' + item.Avartar + '" class="media-object">'
                html += '</a>'
                html += '<div class="media-body">'
                html += '<h3><a href="#">' + item.UserName + '</a></h3>'
                html += '<h4>Excellent course!</h4>'
                html += '<p><h4>Mark:</h4> <h4> ' + item.Mark + '</h4></p>'
                html += '<p>' + item.CommentText + '</p>'
                html += '</div>'
                html += '</div>';
            });
            $('#list').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    //$('#btnUpdate').hide();
    //$('#btnAdd').show();
    return false;
}

function _add() {
    var obj = {
        UserName: $('#name').val(),
        PassWord: $('#password').val(),
        PostId: $('#PostId').val(),
        Mark: $('#mark').val(),
        CommentText: $("#commentText").val(),
    }
    if (confirm('Are you sure you want to create this?')) {
        $.ajax({
            url: '/Comment/Create',
            data: JSON.stringify(obj),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                _getAllComment(id);
                $('#name').val("");
                $('#password').val("");
                $('#mark').val("");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}