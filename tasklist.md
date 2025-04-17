# Tasklist

## Backend

Amund
- _players kan ha spillere som er inactive som m� tenkes p� n�r man spinner, eller endre

Dennis
- BeerPrices lagres i DB (Se model i Domain)
- BackgroundService for � hente ut data, lagre i db
- Logger service med enum tags for error level

- F� inn warnings as errors og ubrukte variabler som feil

## Frontend
- Modal som vises om versjon er utdatert
- varslinger
- Firebase Analysis
- Service
- Connection provider
		- Must also display error on wifi disconnect
		- Must have a api call to remove the player from the game (set to inactive)

## DB Optimalisering
- Pooling
- Indeksering
- Cache
- Host db with backend
