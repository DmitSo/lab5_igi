askServerAndRenderList('Rooms/SortedList', 0);

$('#sort-first').click(function () {
    askServerAndRenderList('Rooms/SortedList', $('#currentPage').val());
    saveToSession('Rooms/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Rooms/SaveFiltration');
});

filterList();