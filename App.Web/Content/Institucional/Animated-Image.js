
$(document).ready(function () {
    var existResizeEvent = false;


    //Animacion cuando se escrolea hacia la imagen.
    function scrollPage() {
        $(window).on("scroll", function () {
            var isVisibleNow = false;
            var isVisibleRightNow = false;
            var images = $(".rotateConainer").find("img");
            if ($(window).scrollTop() > $("#sampleImages").offset().top - $("#sampleImages").height()) {
                if (!isVisibleNow) {

                    $(images[0]).addClass("rotateInLeft");
                    
                    isVisibleNow = true;
                }

            } else {
                $(images[0]).removeClass("rotateInLeft");

                isVisibleNow = false;
            }

            if ($(window).scrollTop() > $("#sampleImages-2").offset().top - $("#sampleImages-2").height()) {
                if (!isVisibleRightNow) {

                    $(images[1]).addClass("rotateInRight");
                    
                    isVisibleRightNow = true;
                }

            } else {
                $(images[1]).removeClass("rotateInRight");

                isVisibleRightNow = false;
            }
        });

    }






    $(window).on("resize", function () {
        if ($(window).width() > 900) {
            if (!existResizeEvent) {

                scrollPage();
                existResizeEvent = true;
            }
        } else {
            $(window).off("scroll");
            existResizeEvent = false;
        }
    })



    scrollPage();
});



jQuery(document).ready(function () {
    $("#freeTestButton").on("click", function () {
        var urlGym = "http:\\\\gym.e-partners.com.ar\\";
        $(location).attr('href', urlGym);
    })
});