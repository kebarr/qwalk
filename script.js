$(document).ready(function(){
    $("#runWalk").click(function(){
        var left = $("#left").val();
        var right = $("#right").val();
	//alert("You selected left coin state amplitude: " + left + "and right coin state amplitude" + right);
    });
    // this needs to send the inputs to a controller, 
    //for time being just make it write something on the page when this has worked. 
    //when this function has recieved the result, it should send it to another function which plots it.
    // doesn't alert anything.... need to work out what URL to use.
    $.post('/inputcontroller/ContentResultTest', function (data) {
        alert("Data Loaded: " + data);
    });
    $.post('/inputcontroller/Walk', left, right, function () {
    });
  });
