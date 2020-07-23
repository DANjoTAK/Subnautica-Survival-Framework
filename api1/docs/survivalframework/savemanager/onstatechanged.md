#{} (pre:${type:event}) OnStateChanged (post:${static} ${addedin:0})

| Parent |
| :---: |
| [${type:class} SaveManager](${currentapidocsurl}survivalframework/savemanager) |

##{id:tableofcontents;} Table of Contents

- [Description](#description)
- [Type](#type)
- [Examples](#examples)

##{id:description;} Description (post:${backtotop})

Fires once the saving/loading state changes.<br>
See [${type:enum} StateChangedEventType](${currentapidocsurl}survivalframework/savemanager/statechangedeventtype) for a list of possible states.

##{id:type;} Type (post:${backtotop})

[${type:delegate} StateChangedEvent](${currentapidocsurl}survivalframework/savemanager/statechangedevent)

##{id:examples;} Examples (post:${backtotop})

```C#
SurvivalFramework.SaveManager.OnStateChanged += args =>
{
    if (args.type == SaveManager.StateChangedEventType.loadingCompleted)
    {
        // The stats were successfully loaded.
        doSomething();
    }
};
```