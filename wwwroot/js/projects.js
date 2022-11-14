//define namespace
var Projects = Projects || {};

$(document).ready(function () {

    //make sure image is added when creating a project
    $('#create_project_button').on('click', function (e) {
        if ($('#Project_ImageURL').val() == '') {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Please upload an Image for this project!'
            });
            return false;
        }
        return true;
    });

    $('#admin_project_table').on('click', '#delete_project_button', function (e) {
        var projectID = $(this).data("projectid");
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: Global.basePath + 'Project/Delete',
                    type: 'DELETE',
                    dataType: 'json',
                    data: { id : projectID },
                    success: function (data) {
                        if (data.success) {
                            window.location.reload();
                            toastr.success(data.message);
                        } else {
                            window.location.reload();
                            toastr.error(data.message);
                        }
                    }
                })
            }
        })
    });

    //wysiwyg
    //$('#Project_Description').summernote();
});