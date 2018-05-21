askServerAndRenderList('RoomTypes/SortedList', 0);

$('#sort-first, #sort-second').click(function () {
    askServerAndRenderList('RoomTypes/SortedList', $('#currentPage').val());
    saveToSession('RoomTypes/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('RoomTypes/SaveFiltration');
});

filterList();