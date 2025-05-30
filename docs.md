# Docs

## Adding a new game

### Controllers
Create endpoint needs to return a `CreateGameResponse` for frontend to route correctly.
The `UniversalId` in this context needs to be one higher than the highest number in the other games.
- `AskGame` has 1.
- `SpinGame` has 2.