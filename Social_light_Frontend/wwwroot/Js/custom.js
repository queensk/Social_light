// window.triggerFileInputClick = (element) => {
//     element.click();
// }


window.triggerFileInputClick = function (inputId) {
    var inputElement = document.getElementById(inputId);
    if (inputElement) {
        console.log("clicked");
        inputElement.click();
    }
};
function readFile(fileInput) {
    return new Promise((resolve, reject) => {
      // Get the file object
      var file = fileInput.files[0];
      // Create a file reader
      var reader = new FileReader();
      // Set the onload function
      reader.onload = function(e) {
        // Resolve the promise with the file data
        resolve(e.target.result);
      };
      // Set the onerror function
      reader.onerror = function(e) {
        // Reject the promise with the error
        reject(e);
      };
      // Read the file as a data URL
      reader.readAsDataURL(file);
    });
  }
  