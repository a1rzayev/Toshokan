console.log('identity-validation.js loaded');
console.log('jQuery version:', typeof $ !== 'undefined' ? $.fn.jquery : 'NOT LOADED');

if (document.getElementById('loginForm')) {
    $(document).ready(function () {
        console.log('Login form JS running');
        console.log('Login: #email exists:', $('#email').length);
        console.log('Login: #password exists:', $('#password').length);
        console.log('Login: #submitButton exists:', $('#submitButton').length);
        $('#togglePassword').on('click', function() {
            const passwordField = $('#password');
            const icon = $(this).find('i');
            if (passwordField.attr('type') === 'password') {
                passwordField.attr('type', 'text');
                icon.removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                passwordField.attr('type', 'password');
                icon.removeClass('fa-eye-slash').addClass('fa-eye');
            }
        });
        $('#loginForm input').on('keyup change', function () {
            validateLoginForm();
        });
        function validateLoginForm() {
            let isValid = true;
            let errors = [];
            const email = $('#email').val().trim();
            const password = $('#password').val();
            console.log('Login validation:', { email, password });
            const emailRegex = new RegExp('^[^\\s' + '@' + ']+@[^\\s' + '@' + ']+\\.[^\\s' + '@' + ']+$');
            if (email === '') {
                isValid = false;
                errors.push('Email is required');
            } else if (!emailRegex.test(email)) {
                isValid = false;
                errors.push('Please enter a valid email address');
            }
            if (password === '') {
                isValid = false;
                errors.push('Password is required');
            }
            $('#submitButton').prop('disabled', !isValid);
            if (isValid) {
                $('#submitButton').removeClass('btn-secondary').addClass('btn-primary');
            } else {
                $('#submitButton').removeClass('btn-primary').addClass('btn-secondary');
            }
            if (!isValid) {
                $('#submitButton').css({
                    'background-color': '#6c757d',
                    'border-color': '#6c757d',
                    'color': '#fff',
                    'opacity': '0.65',
                    'cursor': 'not-allowed'
                });
            } else {
                $('#submitButton').css({
                    'background-color': '',
                    'border-color': '',
                    'color': '',
                    'opacity': '',
                    'cursor': ''
                });
            }
        }
        $('#loginForm').on('submit', function(e) {
            if (!$('#submitButton').prop('disabled')) {
                $('#submitButton').html('<i class="fas fa-spinner fa-spin mr-2"></i>Signing In...');
                $('#submitButton').prop('disabled', true);
            }
        });
    });
}

if (document.getElementById('registrationForm')) {
    $(document).ready(function () {
        console.log('Registration form JS running');
        $('#togglePassword').on('click', function() {
            const passwordField = $('#password');
            const icon = $(this).find('i');
            if (passwordField.attr('type') === 'password') {
                passwordField.attr('type', 'text');
                icon.removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                passwordField.attr('type', 'password');
                icon.removeClass('fa-eye-slash').addClass('fa-eye');
            }
        });
        $('#avatar').on('change', function() {
            const fileName = $(this).val().split('\\').pop();
            $(this).next('.custom-file-label').html(
                fileName ? '<i class="fas fa-file-image mr-2"></i>' + fileName :
                '<i class="fas fa-upload mr-2"></i>Choose file (JPG, PNG, GIF)'
            );
            const file = this.files[0];
            if (file && file.size > 5 * 1024 * 1024) {
                alert('File size must be less than 5MB');
                $(this).val('');
                $(this).next('.custom-file-label').html('<i class="fas fa-upload mr-2"></i>Choose file (JPG, PNG, GIF)');
            }
            validateRegistrationForm();
        });
        $('#registrationForm input').on('keyup change', function () {
            validateRegistrationForm();
        });
        function validateRegistrationForm() {
            let isValid = true;
            let errors = [];
            const name = $('#name').val().trim();
            if (name === '') {
                isValid = false;
                errors.push('First name is required');
            }
            const surname = $('#surname').val().trim();
            if (surname === '') {
                isValid = false;
                errors.push('Last name is required');
            }
            const email = $('#email').val().trim();
            const emailRegex = new RegExp('^[^\\s' + '@' + ']+@[^\\s' + '@' + ']+\\.[^\\s' + '@' + ']+$');
            if (email === '') {
                isValid = false;
                errors.push('Email is required');
            } else if (!emailRegex.test(email)) {
                isValid = false;
                errors.push('Please enter a valid email address');
            }
            const password = $('#password').val();
            if (password === '') {
                isValid = false;
                errors.push('Password is required');
            } else if (password.length < 8 || !/[A-Z]/.test(password)) {
                isValid = false;
                errors.push('Password must be at least 8 characters with one uppercase letter');
            }
            $('#submitButton').prop('disabled', !isValid);
            if (isValid) {
                $('#submitButton').removeClass('btn-secondary').addClass('btn-success');
            } else {
                $('#submitButton').removeClass('btn-success').addClass('btn-secondary');
            }
            if (!isValid) {
                $('#submitButton').css({
                    'background-color': '#6c757d',
                    'border-color': '#6c757d',
                    'color': '#fff',
                    'opacity': '0.65',
                    'cursor': 'not-allowed'
                });
            } else {
                $('#submitButton').css({
                    'background-color': '',
                    'border-color': '',
                    'color': '',
                    'opacity': '',
                    'cursor': ''
                });
            }
        }
        $('#registrationForm').on('submit', function(e) {
            if (!$('#submitButton').prop('disabled')) {
                $('#submitButton').html('<i class="fas fa-spinner fa-spin mr-2"></i>Creating Account...');
                $('#submitButton').prop('disabled', true);
            }
        });
    });
} 