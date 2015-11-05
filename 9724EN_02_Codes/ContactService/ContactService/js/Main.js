function returnContacts() {
    $.ajax({
        type: "GET",
        url: "ContactCRUD.svc/Contacts",
        data: "{}",
        cache:false,
        success: function (data) {
            updateTable(data);
        },
        error: function (msg) {
            alert(msg);
        }
    });
}
function returnContact() {
    var roll = $("#roll").val();
    $.ajax({
        type: "GET",
        url: "ContactCRUD.svc/contact/" + roll,
        data: "{}",
        cache: false,
        success: function (data) {
            updateTable(data);
        },
        error: function (msg) {
            alert(msg);
        }
    });
}
function updateTable(data) {
    $("#output").find("tr:gt(0)").remove();
    $(data).find('Contact').each(function () {
        var roll = $(this).find('Roll').text();
        var name = $(this).find('Name').text();
        var age = $(this).find('Age').text();
        var address = $(this).find('Address').text();
        $('<tr></tr>').html('<th>' + roll + '</th><td>' + name + '</td><td>' + age + '</td><td>' + address + '</td>').appendTo('#output');
    });
}

function postContact() {
    var postObj = "<Contact><Address>" + $("#address").val() + "</Address><Age>" + $("#age").val() + "</Age><Name>" + $("#name").val() + "</Name><Roll>" + $("#roll").val() + "</Roll></Contact>";
    $.ajax({
        type: "POST",
        url: "ContactCRUD.svc/contacts",
        contentType: "application/xml; charset=utf-8",
        data: postObj,
        cache: false,
        success: function (data) {
            returnContacts();
            $(":text").val('');
        },
        error: function (msg) {
            alert(msg);
        }
    });
}
function putContact() {
    var postObj = "<Contact><Address>" + $("#address").val() + "</Address><Age>" + $("#age").val() + "</Age><Name>" + $("#name").val() + "</Name><Roll>" + $("#roll").val() + "</Roll></Contact>";
    $.ajax({
        type: "PUT",
        url: "ContactCRUD.svc/contact",
        contentType: "application/xml; charset=utf-8",
        data: postObj,
        cache: false,
        success: function (data) {
            returnContacts();
        },
        error: function (msg) {
            alert(msg);
        }
    });
}
function deleteContact() {
    var roll = $("#roll").val();
    $.ajax({
        type: "DELETE",
        url: "ContactCRUD.svc/contact/" + roll,
        cache: false,
        success: function (data) {
            returnContacts();
        },
        error: function (msg) {
            alert(msg);
        }
    });
}