askServerAndRenderList('Employees/SortedList', 0);

$('#sort-first').click(function () {
    askServerAndRenderList('Employees/SortedList', $('#currentPage').val());
    saveToSession('Employees/SaveFiltration');
});

$('#find').keyup(function () {
    filterList();
    saveToSession('Employees/SaveFiltration');
});

filterList();