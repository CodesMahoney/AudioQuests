-------------------------------------------------------------------------------
-- the play button when the quest is first introduced -------------------------
-------------------------------------------------------------------------------
local f = CreateFrame("Frame", "AQFrame", QuestFrameDetailPanel)
f:SetPoint("BOTTOMLEFT")
f:SetSize(338, 50)
f:Show()

local button = CreateFrame("Button", "AQButton", f)

button:SetPoint("BOTTOMLEFT", QuestFrameDetailPanel, "BOTTOMLEFT", 85, -7)
button:SetWidth(150)
button:SetHeight(32)

button:SetNormalTexture("Interface/Buttons/UI-Panel-Button-Up")
button:SetHighlightTexture("Interface/Buttons/UI-Panel-Button-Highlight")
button:SetPushedTexture("Interface/Buttons/UI-Panel-Button-Down")

function AQ_QuestButton_OnClick()
    PlaySoundFile("Interface\\AddOns\\AudioQuests\\sounds\\" .. GetQuestID() .. ".mp3");
end

function AQ_QuestButton_OnEnter()
    local fshover = button:CreateFontString("ButtonText2", nil, "GameFontHighlight")
    fshover:SetText("Play Audio")
    fshover:SetPoint("TOPLEFT",15,-4)
    button.text = fshover
end

function AQ_QuestButton_OnLeave()
    local fs = button:CreateFontString("ButtonText", nil, "GameFontNormal")
    fs:SetText("Play Audio")
    fs:SetPoint("TOPLEFT",15,-4)
    button.text = fs
end

AQ_QuestButton_OnLeave()

button:SetScript("OnClick", AQ_QuestButton_OnClick)
button:SetScript("OnEnter", AQ_QuestButton_OnEnter)
button:SetScript("OnLeave", AQ_QuestButton_OnLeave)

-------------------------------------------------------------------------------
-- the play button in the quest log -------------------------------------------
-------------------------------------------------------------------------------
local logF = CreateFrame("Frame", "AQFrameLog", QuestLogPopupDetailFrame)
logF:SetPoint("BOTTOMLEFT")
logF:SetSize(338, 50)
logF:Show()

local logButton = CreateFrame("Button", "AQButton", logF)

logButton:SetPoint("TOPLEFT", QuestLogPopupDetailFrame, "TOPLEFT", 5, -35)
logButton:SetWidth(150)
logButton:SetHeight(32)

logButton:SetNormalTexture("Interface/Buttons/UI-Panel-Button-Up")
logButton:SetHighlightTexture("Interface/Buttons/UI-Panel-Button-Highlight")
logButton:SetPushedTexture("Interface/Buttons/UI-Panel-Button-Down")

function AQ_LogQuestButton_OnClick()
    PlaySoundFile("Interface\\AddOns\\AudioQuests\\sounds\\" .. QuestLogPopupDetailFrame.questID .. ".mp3");
end

function AQ_LogQuestButton_OnEnter()
    local fshover = logButton:CreateFontString("ButtonText2", nil, "GameFontHighlight")
    fshover:SetText("Play Audio")
    fshover:SetPoint("TOPLEFT",15,-5)
    logButton.text = fshover
end

function AQ_LogQuestButton_OnLeave()
    local fs = logButton:CreateFontString("ButtonText", nil, "GameFontNormal")
    fs:SetText("Play Audio")
    fs:SetPoint("TOPLEFT",15,-5)
    logButton.text = fs
end

AQ_LogQuestButton_OnLeave()

logButton:SetScript("OnClick", AQ_LogQuestButton_OnClick)
logButton:SetScript("OnEnter", AQ_LogQuestButton_OnEnter)
logButton:SetScript("OnLeave", AQ_LogQuestButton_OnLeave)

-------------------------------------------------------------------------------
-- the play button in the quest map -------------------------------------------
-------------------------------------------------------------------------------
local mapF = CreateFrame("Frame", "AQFrameMap", QuestMapFrame)
mapF:SetPoint("BOTTOMLEFT")
mapF:SetSize(338, 50)
mapF:Show()

local mapButton = CreateFrame("Button", "AQButton", mapF)

mapButton:SetPoint("TOPRIGHT", QuestMapFrame, "TOPRIGHT", 50, -12)
mapButton:SetWidth(150)
mapButton:SetHeight(32)

mapButton:SetNormalTexture("Interface/Buttons/UI-Panel-Button-Up")
mapButton:SetHighlightTexture("Interface/Buttons/UI-Panel-Button-Highlight")
mapButton:SetPushedTexture("Interface/Buttons/UI-Panel-Button-Down")

function AQ_mapQuestButton_OnClick()
    PlaySoundFile("Interface\\AddOns\\AudioQuests\\sounds\\" .. QuestMapFrame.DetailsFrame.questID .. ".mp3");
end

function AQ_mapQuestButton_OnEnter()
    local fshover = mapButton:CreateFontString("ButtonText2", nil, "GameFontHighlight")
    fshover:SetText("Play Audio")
    fshover:SetPoint("TOPLEFT",15,-5)
    mapButton.text = fshover
end

function AQ_mapQuestButton_OnLeave()
    local fs = mapButton:CreateFontString("ButtonText", nil, "GameFontNormal")
    fs:SetText("Play Audio")
    fs:SetPoint("TOPLEFT",15,-5)
    mapButton.text = fs
end

AQ_mapQuestButton_OnLeave()

mapButton:SetScript("OnClick", AQ_mapQuestButton_OnClick)
mapButton:SetScript("OnEnter", AQ_mapQuestButton_OnEnter)
mapButton:SetScript("OnLeave", AQ_mapQuestButton_OnLeave)
