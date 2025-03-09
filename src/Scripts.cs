namespace FkgNewsViewer
{
    public static class Scripts
    {
        public static string PrintArticle => @"
    (function() {
        var content = document.getElementsByClassName('iframe-content')[0];
        if (content) {
            content.contentWindow.print();
        }
    })();";

        public static string GetMenuMaxPage => @"
    (function() {
        var a = window.frames[0].document.querySelector('#page-list a');
        if (a) {
            var urlParams = new URLSearchParams(a.href);
            return urlParams.get('max_page');
        }
    })();";

        public static string ChangeMenuUrl => @"
    (function() {
        var frame = document.getElementsByClassName('iframe-menu')[0];
        if (frame) {
            frame.contentWindow.location.href = `https://web.flower-knight-girls.co.jp/web/news/menu?page=${page}&platform_type=2&important=0&max_page=${maxPage}`;
        }
    })();";
    }
}
