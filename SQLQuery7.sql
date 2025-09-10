UPDATE Questions
SET QuestionType = 'Clash of Clans'
WHERE QuestionType = 'Multiple Choice'
  AND QuestionID BETWEEN 1 AND 20;

