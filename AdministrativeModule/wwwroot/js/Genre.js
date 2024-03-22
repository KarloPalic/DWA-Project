$(document).ready(function () {
    $(".btn-primary").click(function () {
        var genreId = $(this).closest("tr").find(".genreId").data("genreid");
        saveGenre(genreId);
    });

    $(".btn-danger").click(function () {
        var genreId = $(this).closest("tr").find(".genreId").data("genreid");
        deleteGenre(genreId);
    });

    $(".editable").on("input", function () {
        $(this).addClass("edited");
    });
});

function saveGenre(genreId) {
    var editedElement = $(".editable[data-genreid='" + genreId + "']");

    if (editedElement.hasClass("edited")) {
        var id = genreId;
        var name = editedElement.siblings("[data-field='Name']").text();
        var description = editedElement.siblings("[data-field='Description']").text();

        var genre = {
            Id: id,
            Name: name,
            Description: description
        };

        $.ajax({
            url: '/Genre/UpdateGenre',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(genre),
            success: function () {
                location.reload();
            }
        });

        editedElement.removeClass("edited");
    }
}

function deleteGenre(genreId) {
    $.ajax({
        url: "/Genre/DeleteGenre",
        type: "DELETE",
        contentType: 'application/json',
        data: JSON.stringify(genreId),
        success: function () {
            location.reload();
        }
    });
}