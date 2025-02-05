let DDLforChosen = document.getElementById("selectedOptions");
let DDLforAvail = document.getElementById("availOptions");

/*function to switch list items from one ddl to another
the sender param for the DDL is from which the user is multi-selecting
the receiver param for the DDL is that gets the options*/
function switchOptions(event, senderDDL, receiverDDL) {
    //finds all selected option tags
    let senderID = senderDDL.id;
    let selectedOptions = document.querySelectorAll(`#${senderID} option:checked`);
    event.preventDefault();

    if (selectedOptions.length === 0) {
        alert("Nothing to move.");
    }
    else {
        selectedOptions.forEach(function (o, idx) {
            senderDDL.remove(o.index);
            receiverDDL.appendChild(o);
        });
    }
}
//creating closures so that we can access the event & the 2 parameters
let addOptions = (event) => switchOptions(event, DDLforAvail, DDLforChosen);
let removeOptions = (event) => switchOptions(event, DDLforChosen, DDLforAvail);
//assigning the closures as the event handlers for each button
document.getElementById("btnLeft").addEventListener("click", addOptions);
document.getElementById("btnRight").addEventListener("click", removeOptions);

document.getElementById("btnSubmit").addEventListener("click", function () {
    DDLforChosen.childNodes.forEach(opt => opt.selected = "selected");
})
