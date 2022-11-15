var Register = Register || {}; //Define namespace

$(document).ready(function () {
    $('#registerForm').on('change', '#Input_RoleName', function (e) {
        var selectedRole = $('#Input_RoleName Option:Selected').text();
        if (selectedRole != 'Client') {
            $('#company_select_ctr').hide();
        } else {
            $('#company_select_ctr').show();
        }
    });
});