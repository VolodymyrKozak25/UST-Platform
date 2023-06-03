// Get the role and group elements by their id
let role = document.getElementById("role");
let group = document.getElementById("group");
let label = document.getElementById("groupLabel");

// Add an event listener to the role element to detect changes
role.addEventListener("change", function () {
    // If the role value is teacher, hide the group element and create a new text input element
    if (role.value == "Teacher") {
        group.style.display = "none"; // Hide the group element
        label.innerHTML = "Teacher's key";
        var input = document.createElement("input"); // Create a new input element
        input.id = "groupInput"; // Set the id attribute of the input element
        input.name = "groupInput"; // Set the name attribute of the input element
        input.type = "text"; // Set the type attribute of the input element
        input.className = "form-control"; // Set the class attribute of the input element
        group.parentNode.appendChild(input); // Append the input element to the parent node of the group element
    }
    // If the role value is student, show the group element and remove the text input element
    else if (role.value == "Student") {
        label.innerHTML = "Group";
        group.style.display = "block"; // Show the group element
        var input = document.getElementById("groupInput"); // Get the input element by its id
        if (input) { // If the input element exists, remove it from the DOM
            input.parentNode.removeChild(input);
        }
    }
});