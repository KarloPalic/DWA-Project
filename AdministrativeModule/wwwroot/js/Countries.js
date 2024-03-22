function submitForm(page) {
    var formData = {
        filterByCode: $("#filterByCode").val(),
        page: page
    };

    $.ajax({
        url: $("#filterForm").attr("action"),
        type: "GET",
        data: formData,
        success: function (result) {
            $(".table tbody").html($(result).find(".table tbody").html());
            $(".pagination").html($(result).find(".pagination").html());
        }
    });
}

$("#applyFilters").click(function () {
    submitForm(1);
});