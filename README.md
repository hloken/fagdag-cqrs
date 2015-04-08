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

Exercise 1 - refactoring from CRUD'ish data-model to queries and commands for room booking user story

Currently room-booking has a unified CRUD'ish data model as can be seen in FagdagCqrs.Backend's Data/Adapters/RoomBookingDataAdapter and Data/Models/RoomBooking. The goal of this exercise is to split this into separate data models for commands and queries, at the same time moving towards using queries and commands instead of CRUD.

1. In Data/Adapters, create a folder/namespace "Queries" and a folder/namespace "Commands" - these namespaces will assist in the refactoring work by separating old-style CRUD data-models and new-style CQRS data-models.
2. Using Resharper, move RoomBookingDataAdapter to the new created Commands namespace and rename to RoomBookingCommands.
3. Create a new class "RoomBookingQueries" in the Queries namespace.
4. Move Read(), ReadAll() and the MapToRoomBooking methods from RoomBookingCommands to RoomBookingQueries.
5. Get RoomBookingQueries to compile by adding a field _database and constructor that initializes it (similar to RoomBookingCommands).
6. In BookingModule, add a readonly field for RoomBookingQueries and initialize in a similar way to RoomBookingCommands.
7. Ensure that calls to Read and ReadAll now call the instance of RoomBookingQueries.

We have now separated the room booking-related logic for querying and applying commands to the persistence layer but they still share internal and external contracts. If you run the RestAPI integration-tests and SpecFlow tests they should be green. Let's continue by separating the internal contracts.

8. In the Data/Models folder create a Commands and Queries folder.
9. Using Resharper, move the RoomBooking model to the Models/Queries folder.
10. In the Models/Commands folder, create a new class representing the RoomBooking write model. 
11. Copy all the fields from the Queries/RoomBooking into the Commands/RoomBooking version. Due to the CRUD'ish nature of the data-model it is currently hard to see much difference between the read and write version of a room-booking
12. In RoomBookingCommands, replace all references to Commands/RoomBooking with the new Queries/Roombooking model.

Compile and notice that all the errors now appear in the BookingModule. Mostly this is because our external contracts is the same for commands and queries. 

13. In the Contracts folder create a Commands and Queries folder.
14. Move the BookingInfo-contract to the Contracts/Queries folder.
15. Create a new external contract in the Commands namespace called CreateRoomBooking.
16. In Queries/RoomBookingInfo copy the same six fields as in BookingInfo.
17. In the BookingModule, use CreateRoomBooking in the Post- and (temporarily in the) Put method and fix the parameter types for MapToRoomBooking and RoomBookingCommands.Create.

The last remaining error now is because the command for "confirming" a room booking uses both the read AND the write-model for a room-booking to do a CRUD-ish update on the RoomBooking. This is not very nice, the read-model should never be used in something that is obviously a command.

18. Change the Put["/{bookingId}/confirm"] lambda to use a command rather than CRUD'ish update logic. Simplest way is to introduce a separate command method "ConfirmBooking" or similar on the RoomBookingCommands object and place the logic there. Do it now!

Having split our data-models into Commands and Queries we can make them both more expressive by removing logic and state they no longer use.

19. Implementing booking confirmation as a command rather than an update means that we also can remove the Update method from RoomBookingCommands. Nice!
20. Having removed the need for a common RoomBooking model for Create and Update we could remove it altogether by making the parameters explicit for RoomBookingCommands.Create rather than using a Commands.RoomBooking as parameter.
21. Since the Commands.RoomBooking class is no longer needed remove it and the mapping methods that refer to it.
22. The external contract CreateRoomBooking now contains more fields than it needs. Looking at the parameters passed to RoomBookingCommands.Create, remove the excess fields.


Run the RestAPI and SpecFlow-tests and verify that you haven't broken anything and deploy to production!

(Optional) Exercise 2
1. In the previous exercise, we changed so that the back-end uses separate command and query data-models. Do the same for the client! It might be a good idea to create separate Nancy-modules for RoomBooking Commands and Queries as well.
