# Tasklist

## Backend

- Se på User, kan være det ikke går å lage koblings felles koblingstabell for GuesUser og User
- AddPlayer finnes ikke i game lenger, ta tilbake?? eller kjøre for speed

- Skrive changelog!!
- utvide result pattern til å ha flere messages eller bare en, utvide tli å ha trace og metode for å hente dden øverste
- endre fra early return til liste med feilmeldinger ???
- endepunkt for å oppdatere spillers activity
- endepunkt for å sjekke om spiller finnes
- endepunkt for å gjøre spiller inactive med disconnect
- DDD methods on SpinGame
- Try to combine all assemblies in presenation to get api ts generation
- Performance test AddPlayer DDD vs service layer
- Admin dashboard
	- Toggle notifications on app opening
	- Delete game possibility
	- Se antall aktive brukere nå, denne uken, denne måneden

## Frontend
- Endre InfoModal til å eksportere to funksjoner, toggle error og ikke, ikke bruk bool inn, det er vanskelig å forstå
- Providers
	- Global state
	- Service provider
	- Connection
		- Must have a api call to remove the player from the game (set to inactive)
	- Game specific
- Analysis
	- Firebase

## DB Optimalisering
- Pooling
- Indeksering
- Cache
- Host db with backend
