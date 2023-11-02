

console.log("editAdmin")
// userame validation of admin
function AdminUsername() {


    let username = document.getElementById("adminuser");
    if (username.value.trim() === "") {
        setErrorFor(username, "Username required");
        return false;
    } else {
        setSuccesFor(username);
        return true;
    }
}
// password validation
function AdminPassword() {

    var isPassword = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$/;
    let password = document.getElementById("adminpassword");
    let cpassword = document.getElementById("cpassword");
    if (password.value.trim() === "") {
        setErrorFor(password, "Password required");
        return false;
    } else if (!isPassword.test(password.value.trim())) {
        setErrorFor(password, "Use 8-20 characters and combination of upper and lower");
        return false;
    } else {
        setSuccesFor(password);
        return true;
    }
}


// check both username and password validation
function adminValidation() {

    var usernameadmin = AdminUsername();
    var passwordadmin = AdminPassword();


    if (usernameadmin && passwordadmin) {
        return true;
    } else {
        return false;
    }

}
// to set error in small tag in redcolor

function setErrorFor(input, message) {
    var formParent = input.parentElement;
    var small = formParent.querySelector("small");
    small.className = "adminerror";
    small.innerText = message;
}
// to set no error in smalltag 
function setSuccesFor(input) {
    var formParent = input.parentElement;
    var small = formParent.querySelector("small");
    small.className = "adminsuccess";
    small.innerHTML = "";
}

