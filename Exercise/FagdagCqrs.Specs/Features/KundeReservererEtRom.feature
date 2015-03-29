Egenskap: Kunde reserverer et rom
	Som kunde ønsker jeg å reservere en romtype
		skal kunne reservere for 1 til n dager ved et gitt hotel med en angitt romtype
		skal se totalprisen før bestillingen bekreftes

		jeg skal få en bekreftelse på bestillingen på mail
		det skal reserveres et beløp som tilsvarer 50% av totalprisen på rommet på min bankkonto
		
Scenario: Reservere rom av en gitt type skal se totalpris før jeg bekrefter
	Når jeg reserverer et rom
	| Romtype      | Fra dato  | Lengde på opphold |
	| Junior Suite | 20-1-2015 | 5                 |
	Så skal jeg se totalprisen '5500' før bestillingen bekreftes for reservasjon
	| Romtype      | Fra dato  | Lengde på opphold |
	| Junior Suite | 20-1-2015 | 5                 |

Scenario: Reservere rom av en gitt type skal se reservasjon i listen
	Når jeg reserverer et rom
	| Romtype      | Fra dato  | Lengde på opphold |
	| Junior Suite | 22-1-2015 | 5                 |
	Så skal jeg se reservasjonen i reservasjonslisten
	| Romtype      | Fra dato  | Lengde på opphold |
	| Junior Suite | 22-1-2015 | 5                 |