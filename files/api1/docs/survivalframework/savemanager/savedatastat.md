#{} (pre:${type:class}) SaveDataStat (post:${status:normal} ${addedin:0})

| Parent |
| :---: |
| [${type:namespace} SaveManager](${currentapidocsurl}survivalframework/savemanager) |

##{id:tableofcontents;} Table of Contents

- [Description](#description)
- [Properties](#properties)

##{id:description;} Description (post:${backtotop})

This class is used during saving and loading to help convert a custom stat to JSON and JSON to a custom stat.

##{id:properties;} Properties (post:${backtotop})

###{} (pre:${type:property}) displayName (post:)

${cstype:string}<br>
Defines under what name the stat will be visible to the player.

###{} (pre:${type:property}) hidden (post:)

${cstype:bool}<br>
Defines whether the stat will be visible to the player.

###{} (pre:${type:property}) useMin (post:)

${cstype:bool}<br>
Defines whether the stat has a minimum value.

###{} (pre:${type:property}) useMax (post:)

${cstype:bool}<br>
Defines whether the stat has a maximum value.

###{} (pre:${type:property}) min (post:)

${cstype:float}<br>
Defines the stat's minimum value. This only applies if useMin is true.

###{} (pre:${type:property}) max (post:)

${cstype:float}<br>
Defines the stat's maximum value. This only applies if useMax is true.

###{} (pre:${type:property}) value (post:)

${cstype:float}<br>
Defines the stat's current value.