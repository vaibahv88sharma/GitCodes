
(function () {

    // This code runs when the DOM is ready and creates a context object which is 
    // needed to use the SharePoint object model
    $(document).ready(function () {
        $("#suiteBarDelta").css("display", "none");


        // Hide site until all contents loads
        document.getElementById("initialImage").style.display = "block";
        document.getElementById("s4-bodyContainer").style.display = "none";

    });

})();

