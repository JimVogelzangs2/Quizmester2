-- 1. Voeg de quiz toe (alleen als die nog niet bestaat)
IF NOT EXISTS (SELECT 1 FROM Quizzes WHERE QuizName = 'Clash of Clans Quiz')
BEGIN
    INSERT INTO Quizzes (QuizName) VALUES ('Clash of Clans Quiz');
END

DECLARE @quizId INT = (SELECT QuizID FROM Quizzes WHERE QuizName = 'Clash of Clans Quiz');

-- 2. Voeg 20 vragen toe
INSERT INTO Questions (Question, CorrectAnswer, FalseAnswerOne, FalseAnswerTwo, FalseAnswerThree, QuestionType)
VALUES
('In welk jaar werd Clash of Clans uitgebracht?', '2012', '2010', '2013', '2015', 'Multiple Choice'),
('Hoe heet de eerste troep die je kan trainen in de kazerne?', 'Barbarian', 'Archer', 'Goblin', 'Giant', 'Multiple Choice'),
('Wat is het maximale stadhuisniveau in 2025?', '16', '13', '14', '15', 'Multiple Choice'),
('Welke grondstof wordt gebruikt om gebouwen te upgraden?', 'Gold', 'Elixir', 'Dark Elixir', 'Gems', 'Multiple Choice'),
('Welke spreuk maakt je troepen tijdelijk onzichtbaar?', 'Invisibility Spell', 'Rage Spell', 'Healing Spell', 'Freeze Spell', 'Multiple Choice'),
('Hoe heet de held die je krijgt bij Stadhuis level 5?', 'Barbarian King', 'Archer Queen', 'Grand Warden', 'Royal Champion', 'Multiple Choice'),
('Hoeveel bouwers heb je standaard bij het begin van het spel?', '2', '1', '3', '5', 'Multiple Choice'),
('Wat is de maximale Clan level in 2025?', '30', '20', '25', '35', 'Multiple Choice'),
('Hoeveel trophies heb je minimaal nodig voor de Legend League?', '5000', '4000', '4500', '5500', 'Multiple Choice'),
('Welke troep gebruikt Dark Elixir om te trainen?', 'Minion', 'Archer', 'Giant', 'Wizard', 'Multiple Choice'),
('Welke heldin wordt beschikbaar bij Stadhuis 9?', 'Archer Queen', 'Barbarian King', 'Royal Champion', 'Grand Warden', 'Multiple Choice'),
('Welke spreuk vertraagt vijandige troepen en gebouwen?', 'Poison Spell', 'Freeze Spell', 'Rage Spell', 'Invisibility Spell', 'Multiple Choice'),
('Wat is de hoogste League in Clash of Clans?', 'Legend League', 'Titan League', 'Crystal League', 'Champion League', 'Multiple Choice'),
('Welke troep kan muren overslaan zonder spring spreuk?', 'Hog Rider', 'Barbarian', 'Wizard', 'Healer', 'Multiple Choice'),
('Hoe heet het tweede dorp dat werd toegevoegd in 2017?', 'Builder Base', 'Night Village', 'Dark Base', 'Mini Village', 'Multiple Choice'),
('Welke verdediging schiet zowel op lucht- als grondtroepen?', 'Archer Tower', 'Cannon', 'Mortar', 'Bomb Tower', 'Multiple Choice'),
('Wat is de duurste bron in het spel?', 'Gems', 'Gold', 'Elixir', 'Dark Elixir', 'Multiple Choice'),
('Hoeveel levels heeft de Barbarian King in 2025?', '95', '80', '90', '100', 'Multiple Choice'),
('Welke spreuk herstelt het leven van troepen?', 'Healing Spell', 'Freeze Spell', 'Jump Spell', 'Lightning Spell', 'Multiple Choice'),
('Welke held gebruik je bij Stadhuis 11?', 'Grand Warden', 'Archer Queen', 'Royal Champion', 'Barbarian King', 'Multiple Choice');

-- 3. Koppel vragen aan de quiz
INSERT INTO QuizQuestionsAssociation (QuizID, QuestionID)
SELECT @quizId, QuestionID 
FROM Questions 
WHERE Question IN (
'In welk jaar werd Clash of Clans uitgebracht?',
'Hoe heet de eerste troep die je kan trainen in de kazerne?',
'Wat is het maximale stadhuisniveau in 2025?',
'Welke grondstof wordt gebruikt om gebouwen te upgraden?',
'Welke spreuk maakt je troepen tijdelijk onzichtbaar?',
'Hoe heet de held die je krijgt bij Stadhuis level 5?',
'Hoeveel bouwers heb je standaard bij het begin van het spel?',
'Wat is de maximale Clan level in 2025?',
'Hoeveel trophies heb je minimaal nodig voor de Legend League?',
'Welke troep gebruikt Dark Elixir om te trainen?',
'Welke heldin wordt beschikbaar bij Stadhuis 9?',
'Welke spreuk vertraagt vijandige troepen en gebouwen?',
'Wat is de hoogste League in Clash of Clans?',
'Welke troep kan muren overslaan zonder spring spreuk?',
'Hoe heet het tweede dorp dat werd toegevoegd in 2017?',
'Welke verdediging schiet zowel op lucht- als grondtroepen?',
'Wat is de duurste bron in het spel?',
'Hoeveel levels heeft de Barbarian King in 2025?',
'Welke spreuk herstelt het leven van troepen?',
'Welke held gebruik je bij Stadhuis 11?'
);
