local badelineBoost = {}

badelineBoost.name = "Partline/UBadelineBoost"
badelineBoost.depth = -1000000
badelineBoost.nodeLineRenderType = "line"
--badelineBoost.texture = "objects/badelineboost/idle00"
badelineBoost.nodeLimits = {0, -1}
local variantOptions = { "blue", "orange", "yellow", "green", "gray", "red", "pink" }
local Names = {"sadeline","cadeline","dadeline","radeline","ladeline","hadeline","nadeline"}

badelineBoost.placements = {
    name = "universal Badeline Boost",
    data = {
        lockCamera = true,
        canSkip = false,
        finalCh9Boost = false,
        finalCh9GoldenBoost = false,
        finalCh9Dialog = false,
        variant = "blue"
    }
}
badelineBoost.fieldInformation = {
    variant = {
        options = variantOptions,
        editable = false
    }
}

function badelineBoost.texture(room, entity)
    local index;
    for i, name in ipairs(variantOptions) do
        if name == entity.variant then
            index = i  -- Store the index
            break  -- Exit the loop once found
        end
    end
    return "objects/" .. Names[index] .. "boost/idle00"
end

return badelineBoost