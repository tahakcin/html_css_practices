document.addEventListener("DOMContentLoaded", function () {
    const sendButton = document.getElementById("send-button");
    const textArea = document.getElementById("text-area"); 

    sendButton.addEventListener("click", emptyAlert);

    function emptyAlert(event) {
        // event.preventDefault();
        
        if (textArea == null || textArea.value.trim() == "") {
            alert("Text area cannot be empty.");
            return
        }

        document.getElementById("contact-form").submit();
    }
})



