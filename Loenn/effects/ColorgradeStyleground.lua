local effect = {}

local celesteEnums = require("consts.celeste_enums")
local colorgrades = celesteEnums.color_grades

effect.name = "Rug/ColorgradeStyleground"
effect.canBackground = true
effect.canForeground = true

effect.fieldInformation = {
    colorgrade = {
        options = colorgrades,
        editable = true
    }
}

effect.defaultData = {
    colorgrade = "none",
    immediate = false,
    duration = 2.0,
    onlyOnce = false
}

--return nil --"FemtoHelper/DistortedParallax" is obsolete
return effect
-- yeah btw thanks femto helper