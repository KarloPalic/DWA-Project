$(document).ready(function () {
    function handleCardClick(videoId) {
        window.location.href = "/Home/CardSelection?videoId=" + videoId;
    }

    $(".card-container").on("click", ".card", function () {
        var videoId = $(this).data("video-id");
        handleCardClick(videoId);
    });

    function submitForm(page) {
        var formData = {
            filterByName: $("#filterByName").val(),
        };

        $.ajax({
            url: $("#filterForm").attr("action"),
            type: "GET",
            data: formData,
            success: function (result) {
                $(".card-container").html($(result).find(".card-container").html());
            }
        });
    }

    $("#applyFilters").click(function () {
        submitForm(1);
    });
});