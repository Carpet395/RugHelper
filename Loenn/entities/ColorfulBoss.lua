local badelineBoss = {}

local enums = require("consts.celeste_enums")
local drawableSprite = require("structs.drawable_sprite")

badelineBoss.name = "Rug/ColorfulBadelineBoss"
badelineBoss.depth = 0
badelineBoss.nodeLineRenderType = "line"
badelineBoss.texture = "characters/whiteBadelineBoss/boss00"
badelineBoss.nodeLimits = {0, -1}
badelineBoss.fieldOrder = {
    "x", "y",
    "flag", "setTo",
    "color", "index", "canChangeMusic"
}
badelineBoss.fieldInformation = {
    patternIndex = {
        fieldType = "integer",
        options = enums.badeline_boss_shooting_patterns,
        editable = false
    },
    color = {
        fieldType = "color"
    }
}
badelineBoss.placements = {
    name = "Colorful Badeline Boss",
    data = {
        flag = "",
        color = "ffffff",
        setTo = false,
        patternIndex = 1,
        startHit = false,
        cameraPastY = 120.0,
        cameraLockY = true,
        canChangeMusic = true
    }
}

local function GetColor(color)
    if type(color) == "string" then

        local r = tonumber(color:sub(1, 2), 16) / 255
        local g = tonumber(color:sub(3, 4), 16) / 255
        local b = tonumber(color:sub(5, 6), 16) / 255
        return {r, g, b, 1}
    elseif type(color) == "table" then
        return {color.r or color[1] or 1, color.g or color[2] or 1, color.b or color[3] or 1, color.a or 1}
    end
    return {1, 1, 1, 1}
end

function badelineBoss.sprite(room, entity)
    local spritePreFix = "boss00"
    local sprite = drawableSprite.fromTexture("characters/whiteBadelineBoss/" .. spritePreFix, entity)
    sprite:setColor(GetColor(entity.color))
    return sprite
end

return badelineBoss
