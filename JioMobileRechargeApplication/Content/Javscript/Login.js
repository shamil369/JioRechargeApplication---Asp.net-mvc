
// validation for username
function checkUsername() {

    let username = document.getElementById("usernamelogin");
    if (username.value.trim() === "") {
        setErrorFor(username, "Username required");
        return false;
    } else {
        setSuccesFor(username);
        return true;
    }
}
//validation for password
function checkPassword() {
    let password = document.getElementById("passwordlogin");
    if (password.value.trim() === "") {
        setErrorFor(password, "Password required");
        return false;
    } else {
        setSuccesFor(password);
        return true;
    }

}

// to set error in small tag in redcolor
function setErrorFor(input, message) {
    var formParent = input.parentElement;
    var small = formParent.querySelector("small");
    small.className = "formerror";
    small.innerText = message;
}
// to set no error in smalltag 
function setSuccesFor(input) {
    var formParent = input.parentElement;
    var small = formParent.querySelector("small");
    small.className = "formsuccess";
    small.innerHTML = "";
}
// validation for all above method
function checkValidate() {

    var checkpassword = checkPassword();
    var checkusername = checkUsername();


    if (checkpassword && checkusername) {
        return true;
    } else {
        return false;
    }
}
