//// ===== Scroll to Top ==== 
//$(window).scroll(function () {
//    if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
//        $('#return-to-top').fadeIn(200);    // Fade in the arrow
//    } else {
//        $('#return-to-top').fadeOut(200);   // Else fade out the arrow
//    }
//});
//$('#return-to-top').click(function () {      // When arrow is clicked
//    $('body,html').animate({
//        scrollTop: 0                       // Scroll to top of body
//    }, 500);
//});


$(document).ready(function () {
    // Show or hide the sticky footer button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.go-top').fadeIn(200);
        } else {
            $('.go-top').fadeOut(200);
        }
    });

    // Animate the scroll to top
    $('.go-top').click(function (event) {
        event.preventDefault();

        $('html, body').animate({ scrollTop: 0 }, 300);
    });

    //top bar above nav bar
    $('#navBar1').affix({
        offset: {
            top: $('#banner').height()
        }
    });

    //nav bar fixed
    var menu = $('#navSection1');
    var origOffsetY = menu.offset().top;

    function scroll() {
        if ($(window).scrollTop() >= origOffsetY) {
            $('#navSection1').addClass('sticky');
            $('#contectSection1').addClass('menu-padding');
        } else {
            $('#navSection1').removeClass('sticky');
            $('#contectSection1').removeClass('menu-padding');
        }


    }

    document.onscroll = scroll;
    

});