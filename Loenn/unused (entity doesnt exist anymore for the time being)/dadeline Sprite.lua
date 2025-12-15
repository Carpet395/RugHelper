local sadelineSprite = {}

sadelineSprite.name = "Partline/DadelineSprite"
sadelineSprite.depth = 0
sadelineSprite.justification = {0.5, 1.0}
sadelineSprite.placements = {
    name = "dadeline Sprite",
    data = {
        left = false,
        floating = false
    }
}

function sadelineSprite.scale(room, entity)
    return entity.left and -1 or 1, 1
end

function sadelineSprite.texture(room, entity)
    return "characters/dadeline/idle00"
end

return sadelineSprite
