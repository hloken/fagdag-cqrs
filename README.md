Setup

* IIS (v8+)
* VS2013
* SpecFlow for Visual Studio 2013 extension - in VS2013 open Tools->Extensions and Updates, search for Specflow and install "SpecFlow for Visual Studio 2013"
* Node - browse to https://nodejs.org/ and select install or download

Project:
	Nuget packages: uses VS2013 integrated package restore, should work automagically on first build

	Client project dependencies:
		1. in Powershell, go to FagdagCqrs.Web project root directory
		2. install project dependencies with "npm install"
		3. install grunt: "npm install -g grunt-cli" 
		4. run "grunt" (leave the PowerKjell window open, grunt is watching for file changes and doing magic)

	For running from Visual Studio:
		1. Select "FagdagCqrs.Backend" as Startup project
