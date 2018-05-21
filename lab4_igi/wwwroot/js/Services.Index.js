askServerAndRenderList('Services/SortedList',0);

$('#sort-first').click(function () {
    askServerAndRenderList('Services/SortedList', $('#currentPage').val());
    saveToSession('Services/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Services/SaveFiltration');
});

filterList();