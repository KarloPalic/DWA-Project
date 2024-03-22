$(document).ready(function () {
    $(".btn-primary").click(function () {
        var tagId = $(this).closest("tr").find(".tagId").data("tagid");
        saveTag(tagId);
    });

    $(".btn-danger").click(function () {
        var tagId = $(this).closest("tr").find(".tagId").data("tagid");
        deleteTag(tagId);
    });

    $(".editable").on("input", function () {
        $(this).addClass("edited");
    });
});

function saveTag(tagId) {
    var editedElement = $(".editable[data-tagid='" + tagId + "']");
    if (editedElement.hasClass("edited")) {
        var id = tagId;
        var name = editedElement.text();

        var tag = {
            Id: id,
            Name: name
        };

        $.ajax({
            url: '/Tag/UpdateTag',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(tag),
            success: function () {
                location.reload();
            }
        });

        editedElement.removeClass("edited");
    }
}

function deleteTag(tagId) {
    $.ajax({
        url: "/Tag/DeleteTag",
        type: "DELETE",
        contentType: 'application/json',
        data: tagId,
        success: function () {
            location.reload();
        }
    });
}