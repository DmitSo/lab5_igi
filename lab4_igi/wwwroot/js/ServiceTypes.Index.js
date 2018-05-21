askServerAndRenderList('ServiceTypes/SortedList', 0);

$('#sort-first, #sort-second').click(function () {
    askServerAndRenderList('ServiceTypes/SortedList', $('#currentPage').val());
    saveToSession('ServiceTypes/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('ServiceTypes/SaveFiltration');
});

filterList();