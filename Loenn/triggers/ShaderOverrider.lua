local mods = require("mods")
local generateTriggerName = mods.requireFromPlugin("libraries.triggerRenamer")

local trigger = {}

trigger.name = "Rug/ShaderOverrider"

trigger.category = "visual"

trigger.triggerText = generateTriggerName

trigger.nodeLimits = {1, -1}

trigger.placements = {
    name = "Shader Overrider",
    data = {
        multiples = false,
        includePlayer = false
    }
}

trigger.fieldInformation = {
}

return trigger