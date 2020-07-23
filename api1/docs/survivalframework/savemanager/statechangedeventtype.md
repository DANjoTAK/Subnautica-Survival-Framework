#{} (pre:${type:enum}) StateChangedEventType (post:${status:normal} ${addedin:0})

| Parent |
| :---: |
| [${type:namespace} SaveManager](${currentapidocsurl}survivalframework/savemanager) |

##{id:tableofcontents;} Table of Contents

- [Values](#values)

##{id:values;} Values (post:${backtotop})

###{} (pre:${type:enummember}) undefined = 0 (post:${status:unimplemented})

This value acts as fallback and does not serve any purpose.

###{} (pre:${type:enummember}) loadingStarted = 1 (post:)

Fires as soon as the framework starts loading the current savegame.

###{} (pre:${type:enummember}) loadingCompleted = 2 (post:)

Fires as soon as the framework finishes loading the current savegame.

###{} (pre:${type:enummember}) loadingFailed = 3 (post:${status:unimplemented})

This member is not used.

###{} (pre:${type:enummember}) savingStarted = 4 (post:)

Fires as soon as the framework starts saving the current savegame.

###{} (pre:${type:enummember}) savingCompleted = 5 (post:)

Fires as soon as the framework starts finishes the current savegame.

###{} (pre:${type:enummember}) savingFailed = 6 (post:${status:unimplemented})

This member is not used.