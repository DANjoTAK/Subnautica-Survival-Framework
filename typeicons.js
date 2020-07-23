!function () {
    "use strict";

    function plugin(hook, vm) {
        let mediapath = '/' + vm.config.basePath + '_media/';
        //let iconStyle = `height: 1em;vertical-align: text-bottom;`;
        let iconStyle = `height:1em;vertical-align:inherit;`;
        let icons = {
            default: {
                file: 'unknown.svg',
                name: 'Unknown Type',
            },
            class: {
                file: 'class.svg',
                name: 'Class',
            },
            constant: {
                file: 'constant.svg',
                name: 'Constant',
            },
            delegate: {
                file: 'delegate.svg',
                name: 'Delegate',
            },
            enum: {
                file: 'enum.svg',
                name: 'Enum',
            },
            enummember: {
                file: 'enummember.svg',
                name: 'EnumMember',
            },
            event: {
                file: 'event.svg',
                name: 'Event',
            },
            field: {
                file: 'field.svg',
                name: 'Field',
            },
            method: {
                file: 'method.svg',
                name: 'Method',
            },
            namespace: {
                file: 'namespace.svg',
                name: 'Namespace',
            },
            property: {
                file: 'property.svg',
                name: 'Property',
            },
            struct: {
                file: 'struct.svg',
                name: 'Struct',
            },
        };
        icons.const = icons.constant;
        icons.deleg = icons.delegate;
        icons.enummem = icons.enummember;
        icons.enummemb = icons.enummember;
        icons.ev = icons.event;
        icons.namesp = icons.namespace;
        icons.prop = icons.property;
        let cstype = {
            default: {
                name: 'unknown',
                icon: 'default',
                link: '',
            },
            void: {
                name: 'void',
                icon: 'struct',
                link: 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void',
            },
            bool: {
                name: 'bool',
                icon: 'struct',
                link: 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool',
            },
            single: {
                name: 'Single',
                icon: 'struct',
                link: 'https://docs.microsoft.com/en-us/dotnet/api/system.single',
            },
            string: {
                name: 'String',
                icon: 'class',
                link: 'https://docs.microsoft.com/en-us/dotnet/api/system.string',
            },
        };
        cstype.float = cstype.single;
        let unitytype = {
            default: {
                name: 'unknown',
                icon: 'default',
                link: '',
            },
            keycode: {
                name: 'KeyCode',
                icon: 'class',
                link: 'https://docs.unity3d.com/ScriptReference/KeyCode.html',
            },
        };
        cstype.float = cstype.single;

        hook.beforeEach(function (content) {
            return content;
        });

        hook.afterEach(function (html, next) {
            html = html.replace(new RegExp('\\$\\{type:\\w+\\}', 'img'), (substring, args) => {
                let _type = substring.match(new RegExp('(?<=\\$\\{type:)\\w+(?=\\})', 'i'))[0].toLowerCase();
                let type = icons.hasOwnProperty(_type) ? icons[_type] : icons.default;
                return `<img src="${mediapath}${type.file}" alt="${type.name}" title="${type.name}" style="${iconStyle}">`;
            });
            html = html.replace(new RegExp('\\$\\{cstype:\\w+\\}', 'img'), (substring, args) => {
                let _type = substring.match(new RegExp('(?<=\\$\\{cstype:)\\w+(?=\\})', 'i'))[0].toLowerCase();
                let type = cstype.hasOwnProperty(_type) ? cstype[_type] : cstype.default;
                let icon = icons.hasOwnProperty(type.icon) ? icons[type.icon] : icons.default;
                return `<a href="${type.link}" target="_blank" rel="noopener"><img src="${mediapath}${icon.file}" alt="${icon.name}" title="${icon.name}" style="${iconStyle}"> ${type.name}</a>`;
            });
            html = html.replace(new RegExp('\\$\\{unitytype:\\w+\\}', 'img'), (substring, args) => {
                let _type = substring.match(new RegExp('(?<=\\$\\{unitytype:)\\w+(?=\\})', 'i'))[0].toLowerCase();
                let type = unitytype.hasOwnProperty(_type) ? unitytype[_type] : unitytype.default;
                let icon = icons.hasOwnProperty(type.icon) ? icons[type.icon] : icons.default;
                return `<a href="${type.link}" target="_blank" rel="noopener"><img src="${mediapath}${icon.file}" alt="${icon.name}" title="${icon.name}" style="${iconStyle}"> ${type.name}</a>`;
            });
            next(html);
        });

        hook.doneEach(function () {

        });

        hook.mounted(function () {

        });

        hook.ready(function () {

        });
    }
    window.$docsify.plugins.push(plugin);
}();