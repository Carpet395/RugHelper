local colorfulbadelineDummy = {}
local drawableSprite = require("structs.drawable_sprite")

colorfulbadelineDummy.name = "Partline/UBadelineDummy"
colorfulbadelineDummy.depth = -1000000
--badelineBoost.texture = "objects/badelineboost/idle00"
local variantOptions = { "blue", "orange", "yellow", "green", "gray", "red", "pink", "purple", "magenta"}
local Names = {"sadeline","cadeline","dadeline","radeline","ladeline","hadeline","nadeline", "badeline", "sixty"}
local Directories = {"sadeline","cadeline","dadeline","green","ladeline","hadeline","nadeline", "badeline", "sixty"}
local Colors = {"2F69CE", "E38444", "FFE800", "009E2A", "AAC1C1", "D30000", "FF7CA0", "9b41c1", "C43684"}



colorfulbadelineDummy.placements = {
    name = "colorful Badeline Dummy",
    data = {
        variant = "blue", 
        left = false,
        floating = true,
        animation = "",
        flag = "",
        ifset = false,
        render = false
    }
}

colorfulbadelineDummy.fieldInformation = {
    variant = {
        options = variantOptions,
        editable = false
    }
}

colorfulbadelineDummy.fieldOrder = {
    "x", "y",
    "variant", "left", "floating",
    "flag", "ifset",
    "animation", "render"
}

function colorfulbadelineDummy.scale(room, entity)
    return entity.left and -1 or 1, 1
end

function GetColor(entity) 
    local index
    for i, name in ipairs(variantOptions) do
        if name == entity.variant then
            index = i
            break
        end
    end

    if not index then
        return {1, 1, 1}
    end

    local hex = Colors[index]

    local r = tonumber(hex:sub(1, 2), 16) / 255
    local g = tonumber(hex:sub(3, 4), 16) / 255
    local b = tonumber(hex:sub(5, 6), 16) / 255

    return {r, g, b}
end

function colorfulbadelineDummy.sprite(room, entity)
    local index;
    for i, name in ipairs(variantOptions) do
        if name == entity.variant then
            index = i  -- Store the index
            break  -- Exit the loop once found
        end
    end

    local sprites = {}

    local spritePreFix2 = "bangs00"
    local spritePreFix = entity.floating and "jumpSlow03" or "idle00"

    local scale = entity.left and -1 or 1

    --return "characters/" .. Directories[index] .. "/" .. sprite--, characters/play/bangs00
    local sprite = drawableSprite.fromTexture("characters/whiteBadeline/" .. spritePreFix, entity)
    local sprite2 = drawableSprite.fromTexture("characters/player/" .. spritePreFix2, entity)
    --sprite:addPosition(entity.x, entity.y)
    sprite2:addPosition(0, 5)

    sprite:setColor(GetColor(entity))
    sprite2:setColor(GetColor(entity))
    sprite:setScale(scale, 1.0)
    sprite2:setScale(scale, 1.0)
    table.insert(sprites, sprite2)
    table.insert(sprites, sprite)

    return sprites
end

return colorfulbadelineDummy