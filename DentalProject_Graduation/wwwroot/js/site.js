//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.


//var Register_Dialog_Form = document.getElementById("Regiser_Dialago");
//var body_Dialog_Form = document.body;
//var Dialog_Register = document.getElementById("Form_Diaglo")
//Register_Dialog_Form.onclick = function () {
//    body_Dialog_Form.classList.add('overlay');
//    Dialog_Register.classList.remove('hide');
//    Dialog_Register.classList.add('show');
//    console.log("Amir")

//}

//// Action To remove of Age AND level Filed in from regiser
var Select_UserType = document.getElementById("SelectUserType");
var AgePatient = document.getElementById("AgePatient");
var DoctorLevel = document.getElementById("DoctorLevel");
window.onload = function () {
    if (Select_UserType.value === "Patient") {
        AgePatient.classList.remove("DisapearAgeAndLevel");
        DoctorLevel.classList.add("DisapearAgeAndLevel");


    }
    if (Select_UserType.value === "Doctor") {
        AgePatient.classList.add("DisapearAgeAndLevel");
        DoctorLevel.classList.remove("DisapearAgeAndLevel");
    }
}

Select_UserType.onchange = function () {
   
    
    if (Select_UserType.value === "Patient") {
        AgePatient.classList.remove("DisapearAgeAndLevel");
        DoctorLevel.classList.add("DisapearAgeAndLevel");


    }

    if (Select_UserType.value === "Doctor") {
        AgePatient.classList.add("DisapearAgeAndLevel");
        DoctorLevel.classList.remove("DisapearAgeAndLevel");
    }

}


//Check Data Vaild Of Register Form

var registerSubmit = document.getElementById("registerSubmit");
var imageUser = document.getElementById("ImageUser")
var Phone = $("#Phone").val()

registerSubmit.onclick = function (event) {
    if (imageUser.value === "") {
        event.preventDefault();
        document.getElementById("ImageViald").innerHTML = "please Upload Your Photo"
    }
    else if (!isNaN(parseInt(Phone))) {
        event.preventDefault();
        document.getElementById("PhoneVaild").innerHTML = "UnVaild Phone number"
    }
    else if (Select_UserType.value === "Patient") {

        if (document.getElementById("checkAge").value === "") {
            event.preventDefault();
            document.getElementById("AgePatientViald").innerHTML = "Enter Your Age"
        }
        else if (!isNaN(parseInt(document.getElementById("checkAge").value))) {
            document.getElementById("AgePatientViald").innerHTML = "Enter Your Vaild Age"
        }
      

    }

    else if (Select_UserType.value === "Doctor") {
        if (document.getElementById("checkLevel").value === "") {
            event.preventDefault();

            document.getElementById("DoctorLeveltViald").innerHTML = "Enter Your Academic level"
        }
    }

    else {

    }
}

////check Image Extension [png,jpg,bmp,jepg]


