/**
 * Created by Henian.xu on 2014/12/30.
 */
(function ($) {
    $(function () {

        var $nav_list = $('.nav-list');
        /*.nav-list li a 被单击*/
        $nav_list.find('li .dropdown-toggle').bind('click', function (e) {
            var $this = $(this).parent();
            $this.addClass('active').siblings().removeClass('active')
                .find('li.active').removeClass('active');

            var $submenu = $this.children('.submenu');
            if ($submenu.length > 0) {
                e.preventDefault();
            }

            if ($this.parent().hasClass('nav-list-small')) {
                return ;
            }else{
                if ($submenu.hasClass('nav-show') == false) {
                    fn_submenu_show($submenu);
                    fn_submenu_hide($this.siblings().children('.submenu'));
                } else {
                    fn_submenu_hide($submenu);
                }
                //alert($this.children('.submenu').height());
            }

        });
        /*layout 展开&闭合*/
        var $west = $('body').layout('panel', 'west');
        $west.mCustomScrollbar({axis:'y',theme:"minimal-dark"});
        var $expand_west = null;
        $west.panel({
            'onCollapse': function () {
                if ($expand_west == null) {
                    $expand_west = $west.parent().siblings('.layout-expand-west').children('.panel-body');
                }
                $west.find('.nav-list').addClass('nav-list-small');
                $west.children().appendTo($expand_west).end();
                fn_submenu_hide($nav_list.find('li').children('.submenu.nav-show'));
            },
            onExpand: function () {
                if ($expand_west == null) {
                    $expand_west = $west.parent().siblings('.layout-expand-west').children('.panel-body');
                }
                $expand_west.find('.nav-list-small').removeClass('nav-list-small');
                $expand_west.children().appendTo($west);
                fn_submenu_hide($nav_list.find('li').children('.submenu.nav-show'));
            }
        });


        /*展开submenu*/
        var fn_submenu_show = function ($el) {
            var $submenu_height = $el.height();
            $el.height(0).addClass('nav-show').removeClass('nav-hide')
                .animate({height: $submenu_height + 'px'}, "fast", function () {
                    $el.css("height", "auto");
                });
        };
        /*闭合submenu*/
        var fn_submenu_hide = function ($el) {
            $el.animate({height: 0}, "fast", function () {
                $el.addClass('nav-hide').removeClass('nav-show').css("height", "auto");
            });
        };
    });
})(jQuery);