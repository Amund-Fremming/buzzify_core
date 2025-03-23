GameBase
- State (enum) ??

SpinPlayer 
- PK: (GameId, PlayerId)  
- SpinGameId (FK, int)
- PlayerId (FK, int)
- Active (bool)

RoundPlayer 
- PK: (RoundId, PlayerId)  
- SpinRoundId (FK, int) 
- PlayerId (FK, int)
- IsHost (bool)

