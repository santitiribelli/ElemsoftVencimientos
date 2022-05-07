$(function () {
    var timer;
    var blocked2 = false;
    $(document).ajaxStart(function () {
        timer = setTimeout(
            function() {
                if (!window.blocked) {
                    $.blockUI(
                        {
                            baseZ: 999999,
                            message: '<h1><img src="/images/loading2.gif" /></h1>',
                            css: {
                                border: 'none',
                                backgroundColor: 'transparent'
                            },
                            overlayCSS: { backgroundColor: '#505050' }
                        });
                    window.blocked2 = true;
                }
            }, 200);
    })
    .ajaxStop(function () {
        clearTimeout(timer);
        $.unblockUI();
        window.blocked2 = false;
    });
});