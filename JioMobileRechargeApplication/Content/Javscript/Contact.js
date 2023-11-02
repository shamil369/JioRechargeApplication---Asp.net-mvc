// validation for firstname

function contactFirstname() {

    var isName = /^[a-zA-Z]+$/;
    let fname = document.getElementById("hello");
    if (fname.value.trim() == "") {
        setErrorFor(fname, "First name required");
        return false;
    } else if (!isName.test(fname.value.trim())) {
        setErrorFor(fname, "Name cannot be numbers or special characters");
        return false;
    } else {
        setSuccesFor(fname);
        return true;
    }
}
//validation for lastname
function contactLastname() {
    var isName = /^[a-zA-Z]+$/;
    let lname = document.getElementById("bye");
    if (lname.value.trim() == "") {
        setErrorFor(lname, "First name required");
        return false;
    } else if (!isName.test(lname.value.trim())) {
        setErrorFor(lname, "Name cannot be numbers or special characters");
        return false;
    } else {
        setSuccesFor(lname);
        return true;
    }
}
//validation for phone number
function contactPhone() {
    var isNumber = /^[0-9]+$/;
    let number = document.getElementById("fan");
    if (number.value.trim() === "") {
        setErrorFor(number, "Contact number required");
        return false;
    } else if (!isNumber.test(number.value.trim())) {
        setErrorFor(number, "Number cannot be alphabets or special character");
        return false;
    } else {
        setSuccesFor(number);
        return true;
    }
}
//validation for Email
function contactEmail() {
    var isEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
    let email = document.getElementById("contactemail");
    if (email.value.trim() === "") {
        setErrorFor(email, "Email required");
        return false;
    } else if (!(isEmail.test(email.value.trim()))) {
        setErrorFor(email, "Email id is not valid");
        return false;
    } else {
        setSuccesFor(email);
        return true;
    }
}
//validation for description
function contactDescription() {
    let description = document.getElementById("enquirydesc");
    if (description.value.trim() == "") {
        setErrorFor(description, "Description required");
        return false;
    }  else {
        setSuccesFor(description);
        return true;
    }
}
//validation for all above
function contactValidation() {
    var descriptioncontact = contactDescription();
    var emailcontact = contactEmail();
    var phonecontact = contactPhone();
    var lnamecontact = contactLastname();
    var fnamecontact = contactFirstname();

    if (fnamecontact && lnamecontact && phonecontact && emailcontact && descriptioncontact) {
        return true;
    } else {
        return false;
    }
}
// to set error in small tag in redcolor
function setErrorFor(input, message) {
    var formParent = input.parentElement;
    var small = formParent.querySelector("small");
    small.className = "contacterror";
    small.innerText = message;
}
// to set no error in smalltag 
function setSuccesFor(input) {
    var formParent = input.parentElement;
    var small = formParent.querySelector("small");
    small.className = "contactsuccess";
    small.innerHTML = "";
}
