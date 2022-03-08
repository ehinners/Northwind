$(function(){
    // preload audio
    var toast = new Audio('media/toast.wav');
    $('.code').on('click', function(e) {
        e.preventDefault();

        // set toast product and code based on data tags
        var code = $(this).attr('data-code');
        $( "#code" ).html( code );

        var product =  $(this).attr('data-product-name');
        $( "#product" ).html( product );

         // first pause the audio (in case it is still playing)
         toast.pause();
         // reset the audio
         toast.currentTime = 0;
        // play audio
        toast.play();
        $('#toast').toast({ autohide: false }).toast('show');
    });

    // toast is hidden (closed) when escape key is hit
    $(document).keydown(function(e) {
        if (e.key === "Escape") { 
            $('#toast').toast('hide');
       }
   });
});