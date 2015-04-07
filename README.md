Exercises

** Setup **

* IIS (v8+)
* VS2013
* SpecFlow for Visual Studio 2013 extension - in VS2013 open Tools->Extensions and Updates, search for Specflow and install "SpecFlow for Visual Studio 2013"
* Node - browse to https://nodejs.org/ and select install or download
* Pull latest (origin/master): https://github.com/hloken/fagdag-cqrs

Project:
	Nuget packages: uses VS2013 integrated package restore, should work automagically on first build

	Client project dependencies:
		1. in Powershell, go to FagdagCqrs.Web project root directory
		2. install project dependencies with "npm install"
		3. install grunt: "npm install -g grunt-cli" 
		4. run "grunt" (leave the PowerKjell window open, grunt is watching for file changes and doing magic)

	For running from Visual Studio:
		1. Select "FagdagCqrs.Backend" as Startup project or make a shortcut to "bin/debug/FagdagCqrs.Backend.exe"

** Introduction to the code base - may skip to Exercises and use this as reference **

4 projects in solution: 

	FagdagCqrs.BackEnd - Self-hosted Nancy based RestAPI and business logic. 
		Namespaces:
			* ApiModules - Nancy modules implementing Rest API + business-logic
			* Contracts - Contracts JSON serialization
			* Data/Adapters - Data-adapters used for applying query/commands to the database
			* Data/Models - Models used for applying query/commands with the database

	FagdagCqrs.Web - Nancy-based web-site hosted in local IIS. Uses Bower for client-side dependencies and grunt for tooling (mostly injection of js-files at the  moment). Mostly static files, App-folder contains Angular.js based SPA for frontend

	FagdagCqrs.Database - in-memory, database representation, in a normal project this would not be code but schema-definitions + running processes so *not intended for modification in this exercise*

	FagdagCqrs.Tests - integration-tests for RestAPI. Runs against in-process instance of Nancy-Modules so fast!
		Namespaces:
			* Bdd - test base-classes and utilities to give a BDD-style to tests
			* Drivers - encapsulation of restAPI related infrastructure to make the tests nice'r
			* RestAPI - the tests

	FagdagCqrs.Specs - specflow tests for frontend. Remember to run FagdagCqrs.Backend console app or tests won't work. 
		Namespaces(from higher-order abstraction and down):
			* Features - SpecFlow tests
			* Arguments - DTO's between Specflow Feature and Steps
			* Steps - SpecFlow step-definitions, contains work-flow for implementation of test-steps
			* Drivers - abstraction for concrete page navigation/DOM-manipulation or RestAPI-actions
			* Pages - abstraction for a web page, uses AngularBindingAdapter to be less brittle
			* AngularBindingAdapter - abstractions for Angular model bindings and directives

Exercise 1 - separate queries and commands for room booking user story

Currently room-booking has a unified CRUD'ish data model as can be seen in FagdagCqrs.Backend's Data/Adapters/RoomBookingDataAdapter and Data/Models/RoomBooking. Let's split it into separate data models for commands and queries, at the same time moving towards thinking in queries and commands instead of read and writes.

1. In Data/Adapters, create a folder/namespace "Queries" and a folder/namespace "Commands".
2. Using Resharper, move RoomBookingDataAdapter to the new created Commands namespace and rename it to RoomBookingCommands.
3. Create a new class "RoomBookingQueries" in the Queries namespace.
4. Move Read() and ReadAll() methods from RoomBookingCommands to RoomBookingQueries.
5. Get RoomBookingQueries to compile by adding a field _database and constructor that initializes it (similar to RoomBookingCommands).
6. In BookingModule, add a readonly field for RoomBookingQueries and initialize in a similar way to RoomBookingCommands.
7. Ensure that calls to Read and ReadAll now call the instance of RoomBookingQueries.

We have now separated the room booking-related logic for querying and applying commands to the persistence layer but they still share internal and external contracts. If you run the RestAPI integration-tests and SpecFlow tests they should be green. Let's continue by separating the internal contracts.

8. In the Data/Models folder create a Commands and Queries folder.
9. Using Resharper, move the RoomBooking model to the Models/Queries folder.
10. In the Models/Commands folder, create a new class representing the RoomBooking read model. Give it a good name, I can't think of one right now so I'll call it RoomBooking. :-) 
11. Looking at the RoomBooking fields used in the web client, copy from the old RoomBooking model (now the read model) the minimum number of fields required for the RoomBooking write-model to work, since the read-model is completely separate from the write-model we don't need all the fields used for queries.
(Hint: find out where the BookingModule Get-methods is used in the client, they are called from the App/Booking/bookingService.js)
(Hint 2: you need all of them :-)
12. In RoomBookingQueries, repleace all references to Commands/RoomBooking with the new Queries/Roombooking model and do the mapping required to get it to compile.

Compile and notice that all the errors now appear in the BookingModule. Mostly this is because our external contracts is the same for commands and queries. 

13. In the Contracts folder create a Commands and Queries folder.
14. Move the BookingInfo-contract to the Contracts/Commands folder and rename it to RoomBookingCommand.
15. Create a new external contract in the Queries namespace called RoomBookingInfo.
16. In Queries/RoomBookingInfo introduce the 6 same fields as for the internal read model.
17. In the BookingModule, use RoomBookingInfo in all Get-methods.

The last remaining error now is because the command for "confirming" a room booking uses both the read AND the write-model for a room-booking to do a CRUD-ish update on the RoomBooking. This is not very nice, the read-model should never be used in something that is obviously a command.

18. Change the Put["/{bookingId}/confirm"] to use a command rather than CRUD'ish update logic. Simplest way is to introduce a separate command method "ConfirmBooking" or similar on the RoomBookingCommands object and place the logic there. Do it now!
19. Implementing booking confirmation as a command rather than an update means that we also can remove the update method. Nice!
20. As a final cleanup: we now know that our read-models only contain the information needed, can we say the same about the write-models. Remove fields not needed in the write-model, both for the external contracts and and model (note you have to introduce a new abstraction for the RoomBooking in the database since this currently uses the write-model. This would not be a problem in a real-world scenario since database internal persistence models would not be exposed in our code )

Exercise 2
1. In the previous exercise, we changed so that the back-end uses separate command and query data-models. Do the same for the client!
