$(document).ready(function () {
    $('.form-image').click(function () { $('#customFile').trigger('click'); });
    $(function () {
        $('.selectpicker').selectpicker();
    });
    setTimeout(function () {
        $('body').addClass('loaded');
    }, 200);


    window.jQueryModalGet = (url, title) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#form-modal .modal-body').html(res.html);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                },
                error: function (err) {
                    console.log(err);
                }
            });
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex);
        }

    };

    window.jQueryModalPost = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#viewAll').html(res.html);
                        $('#form-modal').modal('hide');
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
            return false;
        } catch (ex) {
            console.log(ex);
        }
        return false;
    };

    window.jQueryModalDelete = form => {
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
                try {
                    $.ajax({
                        type: 'POST',
                        url: form.action,
                        data: new FormData(form),
                        contentType: false,
                        processData: false,
                        success: function (res) {
                            if (res.isValid) {
                                $('#viewAll').html(res.html);
                            }
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                } catch (ex) {
                    console.log(ex);
                }


            }
        });
        //prevent default form submit event
        return false;
    }
});

