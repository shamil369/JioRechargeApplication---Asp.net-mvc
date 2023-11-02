// plan price validation
function planPrice() {
    let price = document.getElementById("planprice");
    if (price.value.trim() === "") {
        setErrorFor(price, "price required");
        return false;
    } else {
        setSuccesFor(price);
        return true;
    }
}
// to check plan validity validation
function planValidity() {

    let validity = document.getElementById("planvalidity");
    if (validity.value.trim() === "") {
        setErrorFor(validity, "Validity required");
        return false;
    } else {
        setSuccesFor(validity);
        return true;
    }
}
// plan data validation
function planData() {


    let data = document.getElementById("plandata");
    if (data.value.trim() === "") {
        setErrorFor(data, "Data required");
        return false;
    } else {
        setSuccesFor(data);
        return true;
    }
}
// voice validation
function planVoice() {

    let voice = document.getElementById("planvoice");
    if (voice.value.trim() === "") {
        setErrorFor(voice, "Voice required");
        return false;
    } else {
        setSuccesFor(voice);
        return true;
    }
}

// sms validation

function planSms() {

    let sms = document.getElementById("plansms");
    if (sms.value.trim() === "") {
        setErrorFor(sms, "Sms required");
        return false;
    } else {
        setSuccesFor(sms);
        return true;
    }
}
// validation all input
function planValidation() {

    var priceplan = planPrice();
    var validityplan = planValidity();
    var dataplan = planData();
    var voiceplan = planVoice();
    var smsplan = planSms();


    if (priceplan && validityplan && dataplan && voiceplan && smsplan ) {
        return true;
    } else {
        return false;
    }
}
// function for showing error in form

function setErrorFor(input, message) {
    var formParent = input.parentElement;
    var small = formParent.querySelector("small");
    small.className = "adminerror";
    small.innerText = message;
}
// for showing no error in form
function setSuccesFor(input) {
    var formParent = input.parentElement;
    var small = formParent.querySelector("small");
    small.className = "adminsuccess";
    small.innerHTML = "";
}
