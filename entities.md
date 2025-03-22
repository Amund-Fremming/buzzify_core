
GameBase
- Id (PK, int)
- UniversalId (string)
- CreatorId (int)
- Name (string)
- NumberOfIterations (int)  
- CurrentIteration (int)
- State (enum) ??

Player
- Id (PK, int)
- Hash (string)

Pagination
- Totalitems (int)
- CurrentPage (int)
- PageSize (int)
- Items (list T)
- HasNextPage (bool)
- HasPrevPage (bool)

CreateGameRequest
- Name (string)
- Type (T)
- - - - - - - - - - - - - - -

SpinGame
- Id (PK, int)  
- UniversalId (string)
- CreatorId (int)
- Name (string)
- State (enum)  
- NumberOfIterations (int)  
- CurrentIteration (int)

SpinPlayer 
- PK: (GameId, PlayerId)  
- SpinGameId (FK, int)
- PlayerId (FK, int)
- Active (bool)

Round 
- Id (PK, int)  
- GameId (FK, int)
- Completed (bool)  

RoundPlayer 
- PK: (RoundId, PlayerId)  
- RoundId (FK, int) 
- PlayerId (FK, int)
- IsHost (bool)

Challenge
- Id (PK, int)  
- RoundId (FK, int) 
- NumberOfParticipants (int)
- Text (string)  
- ReadBeforeSpin (bool)
- Weight (int)  ?????????

- - - - - - - - - - - - - - -

AskGame
- Id (PK, int)  
- UniversalId (string)
- CreatorId (int)
- Name (string)
- NumberOfIterations (int)  
- CurrentIteration (int)

Question
- Id (PK, int)  
- AskGameId (FK, int)  
- Text (string)

- - - - - - - - - - - - - - -
