
-- !! BROKEN, unless you can fix dont use this button !! --


local mods = require("mods")
local utils = require("utils")
local languageRegistry = require("language_registry")

local uiElements = require("ui.elements")
local uiUtils = require("ui.utils")
local widgetUtils = require("ui.widgets.utils")
local collapsable = require("ui.widgets.collapsable")
local forms = require("ui.forms.form")
local altSidesMeta = mods.requireFromPlugin("libraries.RugMeta")

local metaButton = uiElements.group({})

-- Convert meta options into Loenn groups
local function intoGroups(tables)
    local groups = {}
    for _, tb in ipairs(tables) do
        local group = {}
        group.title = "ui.leppa.RugMeta.group." .. tb.title
        local fields = {}
        for _, field in ipairs(tb) do
            table.insert(fields, field[1])
        end
        group.fieldOrder = fields
        table.insert(groups, group)
    end
    return groups
end

local function intoInfos(tables)
    local infos = {}
    for _, tb in ipairs(tables) do
        for _, field in ipairs(tb) do
            local info = {}
            if field.type == "list" then
                info.fieldType = "list"
                info.fields = field.fields
            elseif field[2] == "option" then
                info.fieldType = "string"
                info.editable = false
            else
                info.fieldType = field[2] or "string"
            end
            if #field > 2 and field[3] then
                info.options = field[3]
            end
            infos[field[1]] = info
        end
    end
    return infos
end

local function intoDefaults(tables)
    local defaults = {}
    for _, tb in ipairs(tables) do
        for _, field in ipairs(tb) do
            if field.default == nil then
                defaults[field[1]] = ""
            else
                defaults[field[1]] = field.default
            end
        end
    end
    return defaults
end

local function freshForm(values)
    local groups = intoGroups(altSidesMeta.orderedOptions)
    local form, fields = forms.getFormBody(values, {
        fields = intoInfos(altSidesMeta.orderedOptions),
        groups = groups,
        ignoreUnordered = true
    })

    -- Handle list fields manually (like Parameters)
    for k, info in pairs(altSidesMeta.orderedOptions[1]) do
        if type(info) == "table" and info.type == "list" then
            local listField = uiElements.column({})
            local entries = values[k] or {}
            for idx, entry in ipairs(entries) do
                local entryForm, entryFields = forms.getFormBody(entry, { fields = intoInfos({info.fields}) })
                table.insert(listField.children, collapsable.getCollapsable(k .. " #" .. idx, entryForm))
            end
            -- Add button to append new entries
            table.insert(listField.children, uiElements.button("Add " .. k, function()
                table.insert(values[k], {})
                metaButton.open() -- re-open to refresh
            end))
            form[k] = listField
        end
    end

    return form, fields
end

function metaButton.open(_)
    local language = languageRegistry.getLanguage()
    local windowTitle = "Rug Meta Editor"
    local values = altSidesMeta.loadMeta() or intoDefaults(altSidesMeta.orderedOptions)

    local form, fields = freshForm(values)
    local display = uiElements.scrollbox(form):with(uiUtils.fillHeight(true))

    local window = uiElements.window(windowTitle, display):with({
        x = windowX,
        y = windowY,
        width = 720,
        height = 650,
        updateHidden = true
    })

    if metaButton.parent then
        metaButton.parent:addChild(window)
    end
    widgetUtils.addWindowCloseButton(window)
    return window
end

-- attach menu button
local menubar = require("ui.menubar")
local mapButton = $(menubar.menubar):find(t -> t[1] == "map")
if not $(mapButton[2]):find(e -> e[1] == "leppa_rug_meta") then
    table.insert(mapButton[2], { "leppa_rug_meta", metaButton.open })
end

return metaButton
