local mods = require("mods")
local generateTriggerName = mods.requireFromPlugin("libraries.triggerRenamer")

local trigger = {}

trigger.name = "Rug/DisableCassetteBlocksTrigger"

trigger.category = "visual"

trigger.triggerText = generateTriggerName

trigger.placements = {
    name = "Disable Cassette Blocks Trigger",
    data = {
        onlyOnce = false
    }
}

trigger.fieldInformation = {
}

return trigger