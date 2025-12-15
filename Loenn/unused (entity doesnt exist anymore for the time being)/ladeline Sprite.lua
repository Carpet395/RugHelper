local sadelineSprite = {}

sadelineSprite.name = "Partline/LadelineSprite"
sadelineSprite.depth = 0
sadelineSprite.justification = {0.5, 1.0}
sadelineSprite.placements = {
    name = "ladeline Sprite",
    data = {
        left = false,
        floating = false
    }
}

function sadelineSprite.scale(room, entity)
    return entity.left and -1 or 1, 1
end

function sadelineSprite.texture(room, entity)
    return "characters/ladeline/idle00"
end

return sadelineSprite
