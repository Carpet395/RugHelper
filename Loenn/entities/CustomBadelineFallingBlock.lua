local fakeTilesHelper = require("helpers.fake_tiles")

local fallingBlock = {}

fallingBlock.name = "Rug/CustomBadelineFallingBlock"
fallingBlock.depth = 0
fallingBlock.placements = {
    name = "Custom Badeline falling block",
    data = {
        width = 8,
        height = 8,
        tiletype = fakeTilesHelper.getPlacementMaterial(),
        HighlightTiletype = fakeTilesHelper.getPlacementMaterial(),
        behind = false,
        TileColor = "FFFFFF",
        HighlightColor = "FFFFFF"
    }
}

fallingBlock.fieldInformation = {
    --tiletype = fakeTilesHelper.getFieldInformation("tiletype").tiletype,
    --HighlightTiletype = fakeTilesHelper.getFieldInformation("HighlightTiletype").tiletype,
    tiletype = {
        options = fakeTilesHelper.getTilesOptions(false),
        editable = false
    },
    HighlightTiletype = {
        options = fakeTilesHelper.getTilesOptions(false),
        editable = false
    },
    TileColor = { 
        fieldType = "color" 
    },
    HighlightColor = { 
        fieldType = "color" 
    }
}

fallingBlock.sprite = fakeTilesHelper.getEntitySpriteFunction("tiletype", false)

return fallingBlock