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