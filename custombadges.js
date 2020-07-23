!function () {
    "use strict";

    function plugin(hook, vm) {
        let mediapath = '/' + vm.config.basePath + '_media/';
        var head = document.head || document.getElementsByTagName("head")[0];
        var stylesheet = document.createElement("style");
        stylesheet.type = "text/css";
        head.appendChild(stylesheet);
        let style = `
.custombadge-status {
    display:inline-flex;
    border:solid 3px;
    border-radius:2em;
    padding:0.25em 0.5em;
    font-size:0.5em;
    vertical-align:text-bottom;
    align-items:center;
    text-decoration:none!important;
}
.custombadge-status.circle>.circle {
    width:1em;
    height:1em;
    display:block;
    margin-right:0.4em;
    border-radius:1em;
}
.custombadge-status>.title {
    display: inline-block;
    text-decoration:none!important;
}
.custombadge-status.bold>.title {
    font-weight: bold;
}
.custombadge-addedin {
    display:inline-flex;
    border:solid 3px;
    border-radius:2em;
    padding:0.25em 0.5em;
    font-size:0.5em;
    vertical-align:text-bottom;
    align-items:center;
    text-decoration:none;
    border-color:#03fce7;
    color:#03fce7;
    transition:background-color 0.2s ease-in-out,color 0.2s ease-in-out;
}
.custombadge-addedin:hover {
    background-color:#03fce7;
    color: black;
}
.custombadge-addedin .icon {
    height: 1em;
    vertical-align: inherit;
    margin-right: 0.3em;
}
.custombadge-addedin>a,.custombadge-addedin>a:hover {
    display: inline-flex;
    text-decoration:none;
    align-items: center;
}
.custombadge-static {
    display:inline-flex;
    border:solid 3px;
    border-radius:2em;
    padding:0.25em 0.5em;
    font-size:0.5em;
    vertical-align:text-bottom;
    align-items:center;
    text-decoration:none;
    border-color:#03fce7;
    color:#03fce7;
    transition:background-color 0.2s ease-in-out,color 0.2s ease-in-out;
}
.custombadge-static:not(:hover) {
    background-color:#03fce7;
    color: black;
}
.custombadge-static>a,.custombadge-static>a:hover {
    display: inline-flex;
    text-decoration:none;
    align-items: center;
}
`;
        stylesheet.styleSheet ? stylesheet.styleSheet.cssText = style : stylesheet.appendChild(document.createTextNode(style));
        let elemstatus = {
            default: {
                color: '#ffffff',
                title: 'Normal',
                description: 'This element is up to date and still supported.',
                visible: false,
                circle: false,
                bold: false,
            },
            deprecated: {
                color: '#ff5000',
                title: 'Deprecated',
                description: 'This element has been replaced and will no longer be updated.',
                visible: true,
                circle: false,
                bold: false,
            },
            toberemoved: {
                color: '#ff0000',
                title: 'To be Removed',
                description: 'This element will be removed and will therefore no longer be available in newer versions. Any mods still relying on this have to change this or they will no longer work.',
                visible: true,
                circle: false,
                bold: false,
            },
            removed: {
                color: '#ff0000',
                title: 'Removed',
                description: 'This element has been removed and is therefore no longer available in newer versions. Any mods still relying on this will no longer work.',
                visible: true,
                circle: false,
                bold: false,
            },
            donotuse: {
                color: '#ba0000',
                title: 'Do not use!',
                description: 'Do not use this element! This is not intended for public use and is only public due to some unfortunate circumstances.',
                visible: true,
                circle: false,
                bold: true,
            },
            unimplemented: {
                color: '#797979',
                title: 'Unimplemented',
                description: 'This is currently not implemented and does not do anything.',
                visible: true,
                circle: false,
                bold: false,
            },
        }
        elemstatus.normal = elemstatus.default;
        let versions = {
            default: {
                api: 0,
                update: 0,
                version: '0',
            },
            0: {
                api: 1,
                update: 0,
                version: '1.0',
            },
        };

        hook.init(function () {

        });

        hook.beforeEach(function (content) {
            return content;
        });

        hook.afterEach(function (html, next) {
            html = html.replace(new RegExp('\\$\\{status:\\w+\\}', 'img'), (substring, args) => {
                let _status = substring.match(new RegExp('(?<=\\$\\{status:)\\w+(?=\\})', 'i'))[0].toLowerCase();
                let status = elemstatus.hasOwnProperty(_status) ? elemstatus[_status] : elemstatus.default;
                return status.visible ? `<div class="custombadge-status${status.circle?' circle':''}${status.bold?' bold':''}" style="border-color:${status.color};color:${status.color};" title="${status.description}"><div class="circle" style="background-color:${status.color};"></div><div class="title">${status.title}</div></div>` : '';
            });
            html = html.replace(new RegExp('\\$\\{static\\}', 'img'), (substring, args) => {
                return `<div class="custombadge-static" title="Static"><div class="title">STATIC</div></div>`;
            });
            html = html.replace(new RegExp('\\$\\{addedin:\\w+(:\\w+)?\\}', 'img'), (substring, args) => {
                let _version = substring.match(new RegExp('(?<=\\$\\{addedin:)\\w+(?=(:\\w+)?\\})', 'i'))[0].toLowerCase();
                let version = versions.hasOwnProperty(_version) ? versions[_version] : versions.default;
                let _visible = substring.match(new RegExp('(?<=\\$\\{addedin:\\w+:)\\w+(?=\\})', 'i'));
                let visible = _visible?_visible[0].toLowerCase()==='true':true;
                return visible?`<div class="custombadge-addedin" title="Added in"><a href="#/API${version.api}/changelog?id=${version.update}"><img class="icon" src="${mediapath}addedin.svg"><div class="title">v${version.version} 🞄 u${version.update}</div></a></div>`:'';
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