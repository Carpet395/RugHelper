local mods = require("mods")
local generateTriggerName = mods.requireFromPlugin("libraries.triggerRenamer")

local trigger = {}

trigger.name = "Rug/ReloadTrigger"

trigger.category = "visual"

trigger.triggerText = generateTriggerName

trigger.placements = {
    name = "Reload Level Trigger",
    data = {
        onlyOnce = false
    }
}

trigger.fieldInformation = {
}

return trigger