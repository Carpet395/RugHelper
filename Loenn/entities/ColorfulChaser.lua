local badelineChaser = {}
local drawableSprite = require("structs.drawable_sprite")

badelineChaser.name = "Rug/ColorfulChaser"
badelineChaser.depth = 0
badelineChaser.justification = {0.5, 1.0}
badelineChaser.texture = "characters/badeline/sleep00"
badelineChaser.fieldOrder = {
    "x", "y",
    "flag", "setTo",
    "color", "index", "canChangeMusic"
}
badelineChaser.fieldInformation = {
    color = {
        fieldType = "color"
    }
}
badelineChaser.placements = {
    name = "Colorful Chaser",
    data = {
        index = 0,
        canChangeMusic = true,
        flag = "",
        color = "ffffff",
        setTo = false
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

function badelineChaser.sprite(room, entity)
    local spritePreFix = "sleep22"
    local sprite = drawableSprite.fromTexture("characters/whiteBadeline/" .. spritePreFix, entity)
    sprite:setColor(GetColor(entity.color))
    return sprite
end

return badelineChaser
