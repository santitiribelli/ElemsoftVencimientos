(function ($) {
    
    $.fn.gelemAlert = function (options) {
        var settings = $.extend({
            tipo: 'Alert',
            texto: 'Alerta Error!',
            tiempo: 5000,
            entrada: 'bounceIn',
            salida: 'bounceOut'
        }, options);
        var objeto = $(this);
        objeto.attr('class', '');
        objeto.addClass('alert alert-' + settings.tipo + ' alert-dismissible customAlertPlugin');
        objeto.addClass('animated ' + settings.entrada + '');
        objeto.one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            setTimeout(function () {
                objeto.addClass(settings.salida);
                
            }, settings.tiempo);

            objeto.removeClass(settings.entrada);
        });
        objeto.attr('role', 'alert');
        objeto.text(settings.texto);
    };
}(jQuery));