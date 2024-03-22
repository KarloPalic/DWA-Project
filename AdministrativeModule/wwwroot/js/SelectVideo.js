console.log('Script loaded!');
$(document).ready(function () {
    $('#SelectedVideoName').on('change', function () {
        $('#selectVideoForm').submit();
    });

    $('#saveChangesbtn').on('click', function () {
        console.log("Save button clicked");
        saveChanges();
    });

    $('#deleteVideobtn').on('click', function () {
        console.log("Delete button clicked");
        deleteVideo();
    });
});

function saveChanges() {
    var videoId = $('#videoId').text();
    var videoName = $('#VideoName').val();
    var description = $('#Description').val();
    var genreId = $('#GenreId').val();
    var totalSeconds = $('#TotalSeconds').val();
    var streamingUrl = $('#StreamingUrl').val();
    var imageId = $('#ImageId').val();

    var data = {
        id: videoId,
        name: videoName,
        description: description,
        genreId: genreId,
        totalSeconds: totalSeconds,
        streamingUrl: streamingUrl,
        imageId: imageId
    };

    $.ajax({
        url: '/Home/SaveVideoChanges',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function () {
            alert('Changes saved successfully!');
            location.reload(true);
        },
        error: function (error) {
            console.error('Error saving changes:', error);
        }
    });
}

function deleteVideo() {
    var videoId = $('#videoId').text();

    var data = videoId;

    $.ajax({
        url: '/Home/DeleteVideo',
        type: 'DELETE',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function () {
            alert('Video deleted successfully!');
            location.reload(true);
        },
        error: function (error) {
            console.error('Error deleting video:', error);
        }
    });
}