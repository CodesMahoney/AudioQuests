# AudioQuests

AudioQuestRetriever is a .Net Core Console app that was created to scrape wowdb, generate json with the quest summary and description, and then feed that list to google cloud's text to speech and generate .mp3 files.

Also a website was built in the same .net core solution, originally it was my thought that Wow did not allow playing custom sound files in addons... So i built a web app that you could just type a quest ID into and hear the quest text. Obviously the add on is a more immersive experience so I was glad when I found that indeed you can still load custom sounds from addons.

The actual wow addon is contained in the AudioQuests.toc file as well as the Core.lua file.

Please feel free to make updates if necssary, leave any feedback, or if you have a nack for voice acting, feel free to update some of the mp3 files with your custom voices and if they are decent we'll use those instead.
