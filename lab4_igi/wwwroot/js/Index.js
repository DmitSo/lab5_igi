var filterList = function () {
    var key = $('#find').val();
    var arr = $('.item-line').toArray();
    arr.forEach(function (item) {
        var id = item.getAttribute('id');
        if (!id.startsWith(key)) {
            $('#' + id).hide();
        }
        else {
            $('#' + id).show();
        }
    });
};

var saveToSession = function (urlAdress) {
    $.ajax({
        data: {
            find: $('#find').val(),
            first: $('#sort-first').prop('checked'),
            second: $('#sort-second').prop('checked'),
            third: $('#sort-third').prop('checked')
        },
        url: urlAdress,
        type: 'POST'
    });
}

var askServerAndRenderList = function (urlAdress, pageStartId) {
    $('#list').text('');
    $.ajax({
        data: {
            first: $('#sort-first').prop('checked'),
            second: $('#sort-second').prop('checked'),
            third: $('#sort-third').prop('checked'),
            pageStart: pageStartId
        },
        url: urlAdress,
        success: function (response) {
            $('#list').append(response);
            filterList();
        }
    });
}

var pickPage = function (urlAdress, element) {
    var value = element.val() === '<<' ? +($('#currentPage').val().toString()) - 1 :
        element.val() === '>>' ? +($('#currentPage').val().toString()) + 1 :
            element.val();
    askServerAndRenderList(urlAdress, value);
}