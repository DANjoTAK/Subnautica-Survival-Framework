!function () {
    "use strict";

    function plugin(hook, vm) {


        hook.init(function () {

        });

        hook.beforeEach(function (content) {
            content = content.replace(new RegExp('```\\{[^:\\{\\}]+:[^:\\{\\}]+\\}(?:[^`]|[`]{1,2})+```(?=\\s*(?:\\n+|$))', 'ig'), (substring, args) => {
                let language = substring.match(new RegExp('(?<=```\\{)[^:\\{\\}]+(?=(:[^:\\{\\}]+)?\\})', 'i'))[0];
                let _title = substring.match(new RegExp('(?<=```\\{[^:\\{\\}]+:)[^:\\{\\}]+(?=\\})', 'i'));
                let title = _title ? _title[0] : language;
                let content = substring.match(new RegExp('(?<=```\\{[^:\\{\\}]+:[^:\\{\\}]+\\})(?:[^`]|[`]{1,2})+(?=```)', 'i'))[0];
                content = content.replace(new RegExp('^\\s*', 'i'), '');
                content = content.replace(new RegExp('\\s+$', 'i'), '');
                return `<pre v-pre data-lang="${title}"><code class="lang-${language}">${content}</code></pre>`;
            });
            return content;
        });

        hook.afterEach(function (html, next) {
            next(html);
        });

        hook.doneEach(function () {

        });

        hook.mounted(function () {

        });

        hook.ready(function () {
            //
        });
    }
    window.$docsify.plugins.push(plugin);
}();