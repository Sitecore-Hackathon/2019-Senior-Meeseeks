jQuery( document ).ready(function( $ ) {


	"use strict";


		$('.owl-carousel').owlCarousel({
		    items:4,
		    lazyLoad:true,
		    loop:true,
		    dots:true,
		    margin:20,
		    responsiveClass:true,
			    responsive:{
			        0:{
			            items:1,
			        },
			        600:{
			            items:2,
			        },
			        1000:{
			            items:4,
			        }
			    }
		});

		/* activate jquery isotope */
		  var $container = $('.posts').isotope({
		    itemSelector : '.item',
		    isFitWidth: true
		  });

		  $(window).smartresize(function(){
		    $container.isotope({
		      columnWidth: '.col-sm-3'
		    });
		  });
		  
		  $container.isotope({ filter: '*' });

		    // filter items on button click
		  $('#filters').on( 'click', 'button', function() {
		    var filterValue = $(this).attr('data-filter');
		    $container.isotope({ filter: filterValue });
		});


		$('#carousel').flexslider({
		    animation: "slide",
		    controlNav: false,
		    animationLoop: false,
		    slideshow: false,
		    itemWidth: 210,
		    itemMargin: 5,
		    asNavFor: '#slider'
		});
		 
		$('#slider').flexslider({
		    animation: "slide",
		    controlNav: false,
		    animationLoop: false,
		    slideshow: false,
		    sync: "#carousel"
		});
		
		var endpoint = "/api/Product/TrackClickAccordionSummary";
		
		$(".interaction").on("click", function(){
			
			var data = {
				"productId": $(".single-product").data("productid"),
				"interactionType":$(this).data("interaction")
			};
			console.log(data);
			ajaxRequest("POST", endpoint, data);
				/*request.promise()
        .done(function (data) {

        }.bind(this))//done
        .fail(function (xhr) {
            console.log("fail", xhr);
            swal(ajax_error);
            this.toggle_confirmation();
        }.bind(this));*/
		});
		
    function ajaxRequest(type, url, data) {
        return $.ajax({
            type: type ,
            url: url,
            data: data,
            dataType: 'json',
        });
    }
});
