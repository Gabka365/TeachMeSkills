$(document).ready(function (){
    $('.mostRecent-card').click(function(){
        const card = $(this).closest('.mostRecent-card');			
        const isCurrentCardActive = card.hasClass('active');

        if (isCurrentCardActive) {
            card.removeClass('active');
             card.find('.mostRecent__card-title').css('color', 'var(--darck-color)'); 
        } else {
            $('.mostRecent-card').removeClass('active');
            $('.mostRecent-card .mostRecent__card-title').css('color', 'var(--darck-color)'); 
            card.addClass('active');
            card.find('.mostRecent__card-title').css('color', 'red');
        }
    });

    $('.mostRecent-card').on('mouseover', function() {
        $(this).css('cursor', 'pointer');
    });   
});

