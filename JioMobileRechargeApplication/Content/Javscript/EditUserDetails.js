console.log("hello");


// global declaration for state,cit dateofbirth
var editdob
console.log(sessiondate);
var statedrop;
var citydrop;
var orginalstate = document.getElementById("orgstate");

//validation for state
document.addEventListener("DOMContentLoaded", function () {
    statedrop = document.getElementById("statedrop");
    citydrop = document.getElementById("citydrop");
    console.log(statedrop, "before");

    var state = statedrop.value;
    var citi = cities_state[state];
    citydrop.innerHTML = "";
   
    var option = document.createElement("option");
    option.text = orginalstate.value;
    option.value = orginalstate.value;
        citydrop.appendChild(option)
    

    statedrop.addEventListener("change", function () {
        var selectedstate = statedrop.value;
        var ofstate = selectedstate;
        var cities = cities_state[selectedstate];

        citydrop.innerHTML = "";

        cities.forEach(function (city) {
            var option = document.createElement("option");
            option.text = city;
            option.value = city;
            citydrop.appendChild(option)
        });
        // statedrop.value = ofstate;
       
    });

    citydrop.addEventListener("change", function () {
        orginalstate.value = citydrop.value;
    })
   
});

// Validation for first name

function checkFirstname() {

    var isName = /^[a-zA-Z]+$/;
    let fname = document.getElementById("fname");
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
//validation for last name
function checkLastname() {

    var isName = /^[a-zA-Z]+$/;
    let lname = document.getElementById("lname");
    if (lname.value.trim() == "") {
        setErrorFor(lname, "last name required");
        return false;
    } else if (!isName.test(lname.value.trim())) {
        setErrorFor(lname, "Name cannot be numbers or special characters");
        return false;
    } else {
        setSuccesFor(lname);
        return true;
    }
}
//validation for Date of birth
function checkDateofbirth() {

    function isOld(dateofbirth) {
        var dobs = new Date(dateofbirth);
        var currentDate = new Date();
        var age = currentDate.getFullYear() - dobs.getFullYear();

        if (currentDate.getMonth() < dobs.getMonth() || currentDate.getMonth() === dobs.getMonth() || currentDate.getDate() < dobs.getDate()) {
            age--;
        }
        return age >= 18;
    }

    let dob = document.getElementById("dobedit");
    console.log(dob.value);
 
    if (!isOld(dob.value)) {
        setErrorFor(dob, "date of birth should be greater than 18");
        return false;
    }
    else {
        console.log("gotout");
        setSuccesFor(dob);
        return true;
    }
}
//validation for phonenumber
function checkNumber() {
    var isNumber = /^[0-9]+$/;
    let number = document.getElementById("number");
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
//validation for emasil address
function checkEmail() {
    var isEmail = /^\w+([\.-]?\w+)@\w+([\.-]?\w+)(\.\w{2,3})+$/;
    let email = document.getElementById("email");
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
//validation for Address
function checkAddress() {

    let address = document.getElementById("textaddress");
    if (address.value.trim() === "") {
        setErrorFor(address, "Address is required");
        return false;
    } else {
        setSuccesFor(address);
        return true;
    }
}
//validation for Username
function lookUsername() {

    let username = document.getElementById("uname");
    if (username.value.trim() === "") {
        setErrorFor(username, "Username required");
        return false;
    } else {
        setSuccesFor(username);
        return true;
    }
}
//validation for password
function lookPassword() {

    var isPassword = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$/;
    let password = document.getElementById("password");
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

function checkValidations() {
    var checkfirstname = checkFirstname();
    var checklastname = checkLastname();
    var checkdob = checkDateofbirth();
    var checknumber = checkNumber();
    var checkemail = checkEmail();
    var checkaddress = checkAddress();
    var lookusername = lookUsername();
    var lookpassword = lookPassword();

    if (checkfirstname && checklastname && checkdob && checknumber && checkemail && checkaddress && lookusername && lookpassword) {
        return true;
    } else {
        return false;
    }


}
