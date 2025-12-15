local fakeTilesHelper = require("helpers.fake_tiles")
local drawableRectangle = require("structs.drawable_rectangle")

local dashBlock = {}

dashBlock.name = "Rug/BadAppearBlock"
dashBlock.depth = 0

dashBlock.placements = { 
    {
        name = "Badeline Appear Block",
        data = {
            tiletype = fakeTilesHelper.getPlacementMaterial(),
            flag = "",
            state = true,
            width = 8,
            height = 8,
            onNode = false,
            count = 0,
            Disappear = false
        }
    },
    {
        name = "Badeline Disappear Block",
        data = {
            tiletype = fakeTilesHelper.getPlacementMaterial(),
            flag = "",
            state = true,
            width = 8,
            height = 8,
            onNode = false,
            count = 0,
            Disappear = true
        }
    }
}

dashBlock.fieldInformation = fakeTilesHelper.getFieldInformation("tiletype")

-- Connect logic for tiles (pretty sure is wrong lol)
local function getConnectOptions(entity, room)
    local options = {}
    local onNode = entity.onNode
    local count = tonumber(entity.count) or 0

    -- Go through all other blocks of same type
    for _, e in ipairs(room.entities or {}) do
        if e._name == "Rug/BadAppearBlock" and e ~= entity then
            if (not onNode) or (onNode and e.onNode and e.count == count) then
                table.insert(options, e)
            end
        end
    end

    return options
end

-- Sprite rendering
dashBlock.sprite = function(room, entity)
    -- defaults
    local x = tonumber(entity.x) or 0
    local y = tonumber(entity.y) or 0
    local w = tonumber(entity.width) or 8
    local h = tonumber(entity.height) or 8
    local onNode = entity.onNode == true
    local count = tonumber(entity.count) or 0

    -- tile rendering
    local connectOptions = getConnectOptions(entity, room)
    local sprites = fakeTilesHelper.getEntitySpriteFunction("tiletype")(room, entity, connectOptions)

    -- border
    if onNode then
        local colors = {
            {1,0,0,1}, {1,0.5,0,1}, {1,1,0,1},
            {0,1,0,1}, {0,0,1,1}, {0.7,0,1,1}
        }
        local color = colors[(count % #colors) + 1] or {1,1,1,1}
        local thickness = 1

        table.insert(sprites, drawableRectangle.fromRectangle("line", x, y, w, thickness, color))          -- top
        table.insert(sprites, drawableRectangle.fromRectangle("line", x, y + h - thickness, w, thickness, color)) -- bottom
        table.insert(sprites, drawableRectangle.fromRectangle("line", x, y, thickness, h, color))          -- left
        table.insert(sprites, drawableRectangle.fromRectangle("line", x + w - thickness, y, thickness, h, color)) -- right
    end

    return sprites
end

return dashBlock
