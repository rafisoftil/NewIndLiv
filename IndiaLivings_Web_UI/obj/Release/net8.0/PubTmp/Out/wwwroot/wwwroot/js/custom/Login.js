// Write your JavaScript code.


// Validate Login required fields
function validateLogin_Nulls() {

    // Get values from the input fields
    var phoneNumber = document.getElementById('phone').value;
    var password = document.getElementById('password').value;

    let isValid = true;
        // Check if either field is empty, null, or undefined
        if (phoneNumber === null || phoneNumber === '' ) {
            // Display an error message if either field is invalid
            //alert('The phone number, email, and password cannot be empty or null!');
            const phoneError = document.getElementById("phoneError");
            
            phoneError.textContent = "please enter the phone number or Email";
            phoneError.style.display = "block";

            isValid =  false;

           }
    if (password === null || password === '') {

        const passwordError = document.getElementById("passwordError");

        passwordError.textContent = "please enter the Password";
        passwordError.style.display = "block";


        isValid= false; // Prevent form submission

    }
    
    return isValid; // Proceed with login
}

// Validate login password atleast 6 characters
function validatePassword(password) {

    if (password.length >= 6) {
        return true;
    } else {
        //alert("Password must be exactly 6 characters.");
        const passwordError = document.getElementById("passwordError");

        passwordError.textContent = "Password must be atleast 6 characters";
        passwordError.style.display = "block";
        return false;
    }

}

// Validate proper email address and mobile no
function validate_login() {
    if (!validateLogin_Nulls())
        return false;

        var Error_Message = "";

    // Get the values of the email and phone number fields
    var email = document.getElementById("phone").value;
    var phone = document.getElementById("phone").value;
    var password = document.getElementById("password").value;
    
    if (!validatePassword(password))
        return false;



    let isValid = true;

    // Regular Expression for email validation
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;


    // Regular Expression for phone number validation (simple version)
    const phoneRegex = /^[0-9]{10}$/; // Example: only allows a 10-digit phone number

        let isemail_valid = false;
        let isphone_valid = false;

        // Validate email
        if (emailRegex.test(email)) {
            isemail_valid = true;
        }
        else {
            isValid = false;
            Error_Message += "Please enter a valid email address.";
        }

        if (!isemail_valid) {
            // Validate phone number
            if (phoneRegex.test(phone)) {
                isphone_valid = true;
            }
            else {
                isValid = false;
                Error_Message += "Please enter a valid 10-digit phone number.";
            }

        }
        

        if (isemail_valid || isphone_valid )
            isValid = true;

    // If both are valid, you can submit the form or handle login logic here
        if (isValid) {
            //alert("Form is valid! Proceeding with login...");
            return isValid;
        }
        else {
            //alert(Error_Message);

            const phoneError = document.getElementById("phoneError");

            phoneError.textContent = Error_Message;
            phoneError.style.display = "block";


        }
    return isValid;
        }

// Clear login fields
function clear_signin_fields() {
    document.getElementById('phone').value = "";
    document.getElementById('password').value = "";


    const checkbox = document.getElementById("signin-check");
    checkbox.checked = false;

}

// Hide Login Error Messages
function HideSignIn_ErrorTags(Type) {

    if (Type == "Phone_Email") {
        const phoneError = document.getElementById("phoneError");
        phoneError.style.display = "none";
        
    }
    else if (Type == "Password") {
        const password = document.getElementById("passwordError");
        password.style.display = "none";
    }

}


//Signup Functions
// Vadlidate Confirm password is exactly matched the password
function validate_RepeatPassword() {
    const enteredPassword = document.getElementById("confirmPassword").value;
    const correctPassword = document.getElementById("passwordSignup").value;
    const feedback = document.getElementById("feedback");
    const tick = document.getElementById("tick");

    let isValid = true;
    if (enteredPassword == correctPassword) {
        feedback.textContent = "Password is correct!";
        feedback.classList.remove("invalid");
        feedback.classList.add("valid");
        tick.style.visibility = "visible";  // Show the tick mark
        isValid =  true;
    }
    else {
        feedback.textContent = "Incorrect password!";
        feedback.classList.remove("valid");
        feedback.classList.add("invalid");
        tick.style.visibility = "hidden";  // Hide the tick mark
        isValid =  false;
    }
    return isValid;
}

// validate signup required fields
function validateSignUp_ReqFields() {
    // Get values from the input fields

    var phoneNumber = document.getElementById('phoneSignup').value;
    var password = document.getElementById('passwordSignup').value;

    let isValid = true;
    
    // Check if either field is empty, null, or undefined
    if (phoneNumber == null || phoneNumber == '') {

        const phoneErrorSignup = document.getElementById("phoneErrorSignup");

        phoneErrorSignup.textContent = "please enter the phone number or Email";
        phoneErrorSignup.style.display = "block";

        isValid = false; 
    }
    if (password == null || password == '') {
        const passwordErrorSignup = document.getElementById("passwordErrorSignup");
        passwordErrorSignup.textContent = "please enter the password";
        passwordErrorSignup.style.display = "block";

        isValid = false;
    }


    return isValid; // Proceed with login
}

// validate Signup password atleast 6 characters
function validateSignup_Password(password) {

    if (password.length >= 6) {
        return true;
    } else {
        const passwordErrorSignup = document.getElementById("passwordErrorSignup");
        passwordErrorSignup.textContent = "please enter the password atleast 6 characters";
        passwordErrorSignup.style.display = "block";
        return false;
    }
}

// validate Sign proper email address and phone no
function validate_SignUp() {
    if (!validateSignUp_ReqFields())
        return false;

    var Error_Message = "";

    // Get the values of the email and phone number fields

    var email = document.getElementById("phoneSignup").value;
    var phone = document.getElementById("phoneSignup").value;
    var password = document.getElementById("passwordSignup").value;


    let isValid = true;

    // Regular Expression for email validation
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;


    // Regular Expression for phone number validation (simple version)
    const phoneRegex = /^[0-9]{10}$/; // Example: only allows a 10-digit phone number

    let isemail_valid = false;
    let isphone_valid = false;

    // Validate email
    if (emailRegex.test(email)) {
        isemail_valid = true;
        User_Type = "email";
    }
    else {
        isValid = false;
        //Error_Message += "Please enter a valid email address.";
        const EmailErrorSignup = document.getElementById("EmailErrorSignup");

        EmailErrorSignup.textContent = "Please enter a valid email address.";
        EmailErrorSignup.style.display = "block";

    }


    if (!isemail_valid) {
        // Validate phone number
        if (phoneRegex.test(phone)) {
            isphone_valid = true;
            User_Type = "phone";
        }
        else {
            isValid = false;
            //Error_Message += "Please enter a valid 10-digit phone number.";
            const phoneErrorSignup = document.getElementById("phoneErrorSignup");

            phoneErrorSignup.textContent = "Please enter a valid 10-digit phone number.";
            phoneErrorSignup.style.display = "block";

        }

    }
    

    if (isemail_valid || isphone_valid)
        isValid = true;


    if (!validateSignup_Password(password))
        return false;

    // If both are valid, you can submit the form or handle login logic here
    if (isValid) {
        //alert("Form is valid! Proceeding with login...");
        return isValid;
    }
    else {
        //alert(Error_Message);
    }
    return isValid;
}

// Hide Error Messages 
function HideSignUp_ErrorTags(Type) {

    if (Type == "Phone_Email") {
        const phoneErrorSignup = document.getElementById("phoneErrorSignup");
        phoneErrorSignup.style.display = "none";
        const EmailErrorSignup = document.getElementById("EmailErrorSignup");
        EmailErrorSignup.style.display = "none";
   
    }
    else if (Type == "Password") {
        const passwordErrorSignup = document.getElementById("passwordErrorSignup");
        passwordErrorSignup.style.display = "none";
    }

}

// Clear Signup fields
function clear_signup_fields() {
    document.getElementById('phoneSignup').value = "";
    document.getElementById('passwordSignup').value = "";
    document.getElementById('confirmPassword').value = "";

    const feedback = document.getElementById("feedback");
    const tick = document.getElementById("tick");
    tick.style.visibility = "hidden";  // Hide the tick mark
    feedback.textContent = "";


    const checkbox = document.getElementById("signupCheck");
    checkboxError.style.display = "none";
    checkbox.checked = false;

}

//This script creates a toast dynamically
// Function to display toast message
function showToast(message, type) {

    var toast = document.getElementById("toast-container");

    if (type == "Success") {
        toast.className = "toast_success";
    }
    else if(type == "Failed") {
        toast.className = "toast_failed";

    }

    toast.innerHTML = message;
    document.body.appendChild(toast);


    toast.style.display = "block";
    // Automatically remove the toast after 3 seconds
    setTimeout(function () {
        toast.style.display = "none";
    }, 3000);
}


//To toggle the visibility of a password in a password input field using an eye icon

function fn_toggle_pswd(txtid, toggleid) {

    // Get the elements
    const passwordField = document.getElementById(txtid);
    const togglePassword = document.getElementById(toggleid);

        // Check the current type of the input field
        if (passwordField.type === 'password') {
            // Change type to 'text' to show the password
            passwordField.type = 'text';

        } else {
            // Change type back to 'password' to hide the password
            passwordField.type = 'password';
         }

    document.getElementById(txtid).focus();
    return false;
};
