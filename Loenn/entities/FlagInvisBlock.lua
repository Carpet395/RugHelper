local fakeTilesHelper = require("helpers.fake_tiles")

local dashBlock = {}

dashBlock.name = "Rug/invisBarrier"
dashBlock.depth = 0

function dashBlock.placements()
    return {
        name = "Invis Barrier (flag)",
        data = {
            flag = "",
            state = true,
            width = 8,
            height = 8
        }
    }
end

dashBlock.fillColor = {0.4, 0.4, 0.4, 0.8}
dashBlock.borderColor = {0.0, 0.0, 0.0, 0.0}

return dashBlock