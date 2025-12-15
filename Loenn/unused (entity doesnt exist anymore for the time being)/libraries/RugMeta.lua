local metaHelper = {}

metaHelper.options = {
    { "Effect", "string", default = "Cool/Em" },
}

-- add Parameters as a sub-form list
metaHelper.options.Parameters = {
    title = "Parameters",
    type = "list", -- custom type recognized in our renderer
    fields = {
        { "paramName", "string", default = "" },
        { "paramType", "option", { "string", "float", "int", "bool" }, default = 1 },
        { "paramValue", "string", default = "" }
    }
}

metaHelper.orderedOptions = {
    metaHelper.options
}

---@param table table
---@return number
function metaHelper.tableLength(tbl)
    local count = 0
    for _ in pairs(tbl) do count = count + 1 end
    return count
end

---@param o table
---@return string
function metaHelper.intoYaml(o, ident, blockhint)
    ident = ident or 0
    blockhint = blockhint or false
    if type(o) == 'table' then
        if metaHelper.tableLength(o) == 0 then return "{}" end
        if o[1] ~= nil then
            local s = ""
            for _, v in ipairs(o) do
                s = s .. "\n" .. string.rep(" ", ident) .. "- " .. metaHelper.intoYaml(v, ident + 2, true)
            end
            return s
        end
        local s = ""
        for k, v in pairs(o) do
            s = s .. "\n" .. string.rep(" ", ident) .. tostring(k) .. ": " .. metaHelper.intoYaml(v, ident + 2)
        end
        if blockhint then s = string.sub(s, 2 + ident) end
        return s
    elseif type(o) == "string" then
        return "\"" .. o .. "\""
    else
        return tostring(o)
    end
end

function metaHelper.loadMetaByPath(path)
    local yaml = require("lib.yaml.reader")
    local filesystem = require("utils.filesystem")
    if filesystem.isFile(path) then
        local f = assert(io.open(path, "r"))
        local content = f:read("*all")
        f:close()
        local data = yaml.eval(content)
        if data and data.Sides and data.Sides[1] then data = data.Sides[1] end
        if data and data.Parameters and type(data.Parameters) == "string" then
            local params = {}
            for value in string.gmatch(data.Parameters, '([^,]+)') do table.insert(params, value) end
            data.Parameters = params
        end
        return data
    else return {} end
end

function metaHelper.loadMeta()
    local loadedState = require("loaded_state")
    local baseFile = loadedState.filename
    if not baseFile then return nil end
    local metaFile = string.sub(baseFile, 1, -5) .. ".Rug.meta.yaml"
    return metaHelper.loadMetaByPath(metaFile)
end

function metaHelper.saveMetaToPath(meta, path)
    local toSave = meta
    if meta and meta.Sides and meta.Sides[1] then toSave = meta.Sides[1] end
    if type(toSave.Parameters) == "table" then
        -- save as YAML list of sub-objects
        local newParams = {}
        for _, param in ipairs(toSave.Parameters) do
            table.insert(newParams, {
                paramName = param.paramName,
                paramType = param.paramType,
                paramValue = param.paramValue
            })
        end
        toSave.Parameters = newParams
    end
    local f = assert(io.open(path, "w"))
    f:write(metaHelper.intoYaml(toSave))
    f:close()
end

function metaHelper.saveMeta(meta)
    local loadedState = require("loaded_state")
    local baseFile = loadedState.filename
    if not baseFile then return end
    local metaFile = string.sub(baseFile, 1, -5) .. ".Rug.meta.yaml"
    metaHelper.saveMetaToPath(meta, metaFile)
end

return metaHelper
