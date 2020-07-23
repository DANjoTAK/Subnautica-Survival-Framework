#{} (pre:${type:method}) IsKeyDown(KeyCode key) (post:${static} ${addedin:0})

| Parent |
| :---: |
| [${type:class} InputHelper](${currentapidocsurl}survivalframework/inputhelper) |

##{id:tableofcontents;} Table of Contents

- [Description](#description)
- [Arguments](#arguments)
- [Returns](#returns)
- [Examples](#examples)

##{id:description;} Description (post:${backtotop})

Returns, whether or not the given key is currently pressed. This also verifies that the player is currently ingame and not editing signs or typing in the console.

##{id:arguments;} Arguments (post:${backtotop})

- key : ${unitytype:keycode}

##{id:returns;} Returns (post:${backtotop})

${cstype:bool}

##{id:examples;} Examples (post:${backtotop})

```C#
if (InputHelper.IsKeyDown(KeyCode.N) && !keySleepStartDown) {
    keySleepStartDown = true;
    if (!sleeping) {
        if (NotifyGetCanSleep(Player.main)) {
            if (SettingsManager.keySleepStart == SettingsManager.keySleepStop) keySleepStopDown = true;
            StartSleep(Player.main);
        }
    }
} else if (!InputHelper.IsKeyDown(SettingsManager.keySleepStart) && keySleepStartDown) {
    keySleepStartDown = false;
}
```