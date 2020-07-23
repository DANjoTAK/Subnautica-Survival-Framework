!function () {
    "use strict";

    function plugin(hook, vm) {
        var head = document.head || document.getElementsByTagName("head")[0];
        var stylesheet = document.createElement("style");
        stylesheet.type = "text/css";
        head.appendChild(stylesheet);
        let style = `
h1.custom:not(.underline) a,h1.custom:not(.underline) a:hover {
    text-decoration: none;
}
h1.custom .pre, h2.custom .pre, h3.custom .pre, h4.custom .pre {
    margin-right: 0.3em;
}
h1.custom .post, h2.custom .post, h3.custom .post, h4.custom .post {
    margin-left: 0.3em;
}
h2.custom:not(.underline) a,h2.custom:not(.underline) a:hover {
    text-decoration: none;
}
h2.custom.noline {
    border: none;
    padding: 0;
}
h3.custom:not(.underline) a,h3.custom:not(.underline) a:hover {
    text-decoration: none;
}
h4.custom:not(.underline) a,h4.custom:not(.underline) a:hover {
    text-decoration: none;
}
a.backtotop {
    font-size: 0.5em;
}
`;
        stylesheet.styleSheet ? stylesheet.styleSheet.cssText = style : stylesheet.appendChild(document.createTextNode(style));


        hook.init(function () {

        });

        hook.beforeEach(function (content) {
            content = content.replace(new RegExp('^#+\\{.*\\}.*$', 'img'), (substring, args) => {
                let htype = substring.match(new RegExp('(?<=^)#{1,4}(?=\\{)', 'i'))[0].length;
                if (htype <= 0 || htype > 4) return '';
                let options = substring.match(new RegExp('(?<=^#*\\{)((id|underline|noline):[\\w\\-]+(?=(;)|\\});?)*(?=\\})', 'i'))[0];
                let _id = options.match(new RegExp('(?<=id:)[\\w\\-]+(?=;|$)', 'i'));
                let id = _id ? _id[0] : undefined;
                let _underline = options.match(new RegExp('(?<=underline:)\\w+(?=;|$)', 'i'));
                let underline = _underline ? _underline[0].toLowerCase() === 'true' : false;
                let _noline = options.match(new RegExp('(?<=noline:)\\w+(?=;|$)', 'i'));
                let noline = _noline ? _noline[0].toLowerCase() === 'true' : false;
                let _precontent = substring.match(new RegExp('(?<=^#*\\{.*\\}\\s+\\(pre:)[\\w\\s\\$\\{\\}:]*(?=\\)\\s+.*)', 'i'));
                let precontent = _precontent ? _precontent[0] : undefined;
                let _postcontent = substring.match(new RegExp('(?<=^#*\\{.*\\}.*\\(post:)[\\w\\s\\$\\{\\}:]*(?=\\)\\s*$)', 'i'));
                let postcontent = _postcontent ? _postcontent[0] : undefined;
                let _content = substring.match(new RegExp('(?<=^#*\\{.*\\}\\s+(\\(pre:.*\\)\\s+)?(?!\\(pre:.*\\)\\s+)).*(?=(?<!\\s+\\(post:.*\\)$)(\\s+\\(post:.*\\))?\\s*$)', 'i'));
                let content = _content ? _content[0] : '';
                let currentpath = vm.router.getCurrentPath();
                return `<h${htype} class="custom${underline?' underline':''}${noline?' noline':''}"${id?` id="${id}"`:''}>${precontent?`<span class="pre">${precontent}</span>`:''}<a${id?` href="#${currentpath}?id=${id}" data-id="${id}" class="anchor"`:''}><span>${content}</span></a>${postcontent?`<span class="post">${postcontent}</span>`:''}</h${htype}>`;
            });
            return content;
        });

        hook.afterEach(function (html, next) {
            let currentpath = vm.router.getCurrentPath();
            html = html.replace(new RegExp('\\$\\{link:[^:$]+:[^:$]+\\}', 'img'), (substring, args) => {
                let title = substring.match(new RegExp('(?<=\\$\\{link:)[^:$]+(?=:[^:$]+\\})', 'i'))[0];
                let link = substring.match(new RegExp('(?<=\\$\\{link:[^:$]+:)[^:$]+(?=\\})', 'i'))[0];
                return `<a href="${link}">${title}</a>`;
            });
            html = html.replace(new RegExp('\\$\\{backtotop\\}', 'img'), (substring, args) => {
                return `<a class="backtotop" href="#${currentpath}?id=tableofcontents">↑ Back to top</a>`;
            });
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