$(window).scroll(function () {
    if ($(window).scrollTop() >= 100) {
        $('nav').addClass('fixed-header');
    }
    else {
        $('nav').removeClass('fixed-header');
    }
});
