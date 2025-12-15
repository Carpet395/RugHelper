local enums = require("consts.celeste_enums")

local badelineBoss = {}

badelineBoss.name = "Rug/FlagBadeline"
badelineBoss.depth = 0
badelineBoss.nodeLineRenderType = "line"
badelineBoss.texture = "characters/badelineBoss/charge00"
badelineBoss.nodeLimits = {0, -1}
badelineBoss.fieldInformation = {
    patternIndex = {
        fieldType = "integer",
        options = enums.badeline_boss_shooting_patterns,
        editable = false
    }
}
badelineBoss.placements = {
    name = "badeline boss (flag)",
    data = {
        patternIndex = 1,
        startHit = false,
        cameraPastY = 120.0,
        cameraLockY = true,
        canChangeMusic = true,
        flag = "",
        state = false
    }
}

return badelineBoss