function validateCheckedBoxes() {
    var result = false;
    var checkedNumber = 0;
    var checkBoxes = [];
    var inputs = document.getElementsByTagName("input");

    var i;

    for (i = 0; i < inputs.length; i++) {
        if (inputs[i].type === "checkbox")
            checkBoxes.push(inputs[i]);
    }

    for (i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked === true) {
            checkedNumber += 1;
        }
    }
    if (checkedNumber === 8) {
        result = true;
        document.getElementById("errorMessage").innerHTML = "";
    } else {
        document.getElementById("errorMessage").innerHTML = "Please choose 8 grants from the list";
    }

    return result;
}

function getNumberOfCheckedBoxes() {
    var checkedNumber = 0;
    var checkBoxes = [];
    var inputs = document.getElementsByTagName("input");

    var i;

    for (i = 0; i < inputs.length; i++) {
        if (inputs[i].type === "checkbox")
            checkBoxes.push(inputs[i]);
    }

    for (i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked === true) {
            checkedNumber += 1;
        }
    }
    document.getElementById("demo").innerHTML = checkedNumber;
}