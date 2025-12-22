local mods = require("mods")
local generateTriggerName = mods.requireFromPlugin("libraries.triggerRenamer")

local trigger = {}

trigger.name = "Rug/underererer"

trigger.category = "visual"

trigger.triggerText = generateTriggerName

trigger.placements = {
    name = "Rug/underererer",
    data = {

    }
}

trigger.fieldInformation = {
}

return trigger