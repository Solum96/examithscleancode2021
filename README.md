# Tentamen i Clean code och testbar kod

- Datum: 2021-01-21
- Program: C#-utvecklare
- Lärare: David Sundelius

## Introduktion
Vi har ett system som idag läser en lista på filmer från en json-fil på internet (https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json). Den innehåller titel, id och IMDB-ranking på filerna. Tjänsten som vi utvecklar har två stycken endpoints:
En som returnerar en lista på alla filmers titlar, ordnad efter ranking (stigande eller fallande)
En som returnerar all information vi har om en specifik film utifrån dess id.

## Uppgift
Din uppgift är nu att refaktorera programmet enligt de principer vi gått igenom i kursen. När uppgiften är färdig så ska programmet uppfylla följande egenskaper:
Koden är läsbar och tydlig i enlighet med Clean code-principer.
- Logiken i programmet, såsom sorteringen, är enhetstestad.
- Programmet ska innehålla en rimlig nivå av felhantering, användaren ska få rimliga meddelanden om något är fel.
- Programmet ska innehålla ett motiverat designmönster.
- Koden ska inte innehålla buggen (något som avviker från beskrivningen ovan) som finns i programmet från början.

Utöver detta ska du bygga ut programmet med en ny funktion. Detta är att läsa in data från ytterligare en endpoint (https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json). Denna data ska slås ihop med datan vi redan har och kunna presenteras på samma sätt. Det ska inte förekomma dubbletter av filmer i listan och den ska gå att sortera på samma villkor.

## Hjälpmedel
Alla hjälpmedel är tillåtna: internet, böcker, egenskrivet material och tidigare labbar/annan kod är godkänt exempelvis. Det enda ni inte får göra är att kommunicera med en annan människa under tentans gång, det inkluderar bland annat fysiska personer, telefon och chat.

## Redovisning
Källkoden ska vara pushad till ett eget publikt repository på GitHub. Ert fullständiga namn, samt en *kort* reflektion över dina arkitekturella val, såsom:

- Vad du valt att testa och varför?
- Vilket/vilka designmönster har du valt, varför? Hade det gått att göra på ett annat sätt?
- Hur mycket valde du att optimera koden, varför är det en rimlig nivå för vårt program?

ska finnas med i README.md i repots main-branch (master).


---------------------------------------------------------------------------------------------
## Nils Lundell
Tester:
	Sortering- Enkel att testa men viktig för presentationen
	FindMovieById- Även den lätt att testa men viktig för att api ska funka som specificerat
	CombineMovie- För att undvika dubletter och se till att funktionen bara gör en sak

Pattern:
	Template: För att slippa kladdet med alla JsonSerializers och underlätta för användaren genom att hämta all data med "en" funktion.
	blir svårare att göra fel på det sättet.
	Jag kommer inte på några patterns som bidrar med just den här funktionen.

Optimering:
	Eftersom api:t var ganska litet och simpelt så såg jag till att hålla koden i controllern ganska enkel så att man lätt
	kan förstå vad som händer.
	Detta gör förhoppningsvis programmet lättare att underhålla.
	Jag la även till en extra sortering på namn ifall flera titlar skulle ha samma rating.
