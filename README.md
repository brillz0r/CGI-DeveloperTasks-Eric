# CGI-DeveloperTasks-Eric


## Shape aggregator
Implementerat enligt instruktionerna, även bonusuppgifterna. Eftersom undertecknad inte är särskilt kunnig inom Python så reserverar jag mig för eventuella konstigheter. Jag fokuserade främst på en tydlig struktur.

## BookingsApi
De definierade uppgifterna löstes enligt följande.

1. <em>Tidsöverlapp accepteras ibland. Hitta roten och åtgärda så att testet *CreateBooking_RejectsOverlap2* passerar</em>.

    Jag antog att det istället menades "tidsöverlapp ignoreras ibland" med tanke på vad enhets-testet testade. Detta löstes med att ändra `>` till `>=` för att få med edge case't. Dock inverterade jag hela det logiska uttrycket för att få det mer lättläst.

    Med det sagt, jag fixade även ett eventuellt problem som kunde uppstå enligt er beskrivning. För att förhindra eventuellt flera samtidiga bokningar (vilket då gjorde att tidsöverlapp då kunde accepteras) la jag in ett lås mot detta.

2. `IBookingService` och `IBookingRepository` injiceras nu vid runtime via DI. `IBookingRepository` som en singleton eftersom vi vill att data ska persisteras under hela applikationens livstid (simulerar en databas). `IBookingService` som scoped eftersom den inte behöver leva längre än nuvarande anrop.

3. Implementerade en ny delete endpoint i BookingController enligt instruktionerna.

4. Refaktorerade enligt instruktionerna.

### Övrigt
La stort fokus på felhantering där validering ska ske i business/service-lagret. Jag introducerade ett <em>Result pattern</em> som jag föredrar. Alternativt hade man kunnat implementera <em>custom exceptions</em>, antingen ett exception per specifikt fel som man vill hantera, eller som ett generellt exception med diverse fördefinierade felkoder. Detta kan sedan kombineras med ett ett middleware som fångar dessa exceptions.

Jag introducerade även ett nytt request-objekt vid uppskapning av nya bokningar så kontraktet inte exponerar det interna id't.

De båda projekten är beskrivna i readme-filer i respektive mapp. Dessa skapades med hjälv av Claude.
