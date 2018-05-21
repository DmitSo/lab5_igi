askServerAndRenderList('Clients/SortedList', 0);

$('#sort-first, #sort-second').click(function () {
    askServerAndRenderList('Clients/SortedList', $('#currentPage').val());
    saveToSession('Clients/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Clients/SaveFiltration');
});

filterList();