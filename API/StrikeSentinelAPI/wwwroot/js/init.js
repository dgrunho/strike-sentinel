(function ($) {
    $(function () {

        $('.sidenav').sidenav();
        
        initColapsable()

        
    }); // end of document ready
})

function initColapsable() {
$('.collapsible').collapsible({
            onOpenStart: function () {
                var elems = document.querySelectorAll('.collapsible');
                for (var i = 0; i < elems.length; i++) {
                    var instance = M.Collapsible.getInstance(elems[i]);
                    if (instance != this) {
                        for (var j = 0; j < instance.el.children.length; j++) {
                            instance.close(j)
                        }
                    }
                }
            }
        });
}
    (jQuery); // end of jQuery name space

