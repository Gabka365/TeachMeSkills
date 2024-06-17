$(document).ready(function (){
    
    $('.u-btn').hide();

    $('.u-image').click(function(){         
        if ($(this).hasClass('active')) {            
            $(this).removeClass('active');            
        } else {    
            $('.u-image').removeClass('active');
            $('.u-btn').closest('.custom-expanded').find('.u-btn').hide();            
            $(this).addClass('active');
            $(this).closest('.custom-expanded').find('.u-btn').show();
        }
    });

    $('.u-image').on('mouseover', function() {
        $(this).css('cursor', 'pointer');
    });

    $(document).on('click', function(event) {
        if (!$(event.target).closest('.u-image').length) {
            $('.u-btn').hide();
            $('.u-image').removeClass('active');
        }
    });
});
