The Simple FDF Sample shows how to use the FactSet Datafeed .Net Standard Toolkit to power a .Net Core command line application uses the static RTConsumer (FDF) interface to create a connection to the Datafeed servers and request data for a ticker.

Usage Notes:
  - The output from the solution is a DLL intended to be run with dotnet. To run it use the command `dotnet SimpleFDF.dll <arguments>`
  - The expected arguments for the program are a connection string and a ticker. Calls should be of the form `dotnet SimpleFDF.dll <user-serial>:<password>@<hostname> <ticker>`