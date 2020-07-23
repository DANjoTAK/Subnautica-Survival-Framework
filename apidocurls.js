!function () {
    "use strict";

    function plugin(hook, vm) {
        hook.init(function () {
            // Called when the script starts running, only trigger once, no arguments,
            //vm.route.
        });

        hook.beforeEach(function (content) {
            // Invoked each time before parsing the Markdown file.
            // ...
            //content.replace(new RegExp('\\$\\{currentapiurl\\}', 'gmi'), )
            let currentpath = vm.router.getCurrentPath();
            // \$\{currentapi\}
            let _currentapidocsurl = currentpath.match(new RegExp('^(?:\\/[\\w]+)*\\/docs\\/', 'i'));
            let currentapidocsurl = _currentapidocsurl ? _currentapidocsurl[0] : '/';
            content = content.replace(new RegExp('\\$\\{currentapidocsurl\\}', 'img'), currentapidocsurl);
            content = content.replace(new RegExp('\\$\\{currentapidocsurlf\\}', 'img'), '#' + currentapidocsurl);
            return content;
        });

        hook.afterEach(function (html, next) {
            // Invoked each time after the Markdown file is parsed.
            // beforeEach and afterEach support asynchronous。
            // ...
            // call `next(html)` when task is done.
            next(html);
        });

        hook.doneEach(function () {
            // Invoked each time after the data is fully loaded, no arguments,
            // ...
        });

        hook.mounted(function () {
            // Called after initial completion. Only trigger once, no arguments.
        });

        hook.ready(function () {
            // Called after initial completion, no arguments.
        });
    }
    window.$docsify.plugins.push(plugin);
}();