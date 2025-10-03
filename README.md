# Inoa Project

This is a console app made in .NET that requests the current stock price
and updates the data (in memory) for each requested ticket.

This uses [brapi](https://brapi.dev/) API for data requests.

It's important update your config file (an XML) like the

- App.config.example

configuration.

## Running this project

After the build...

To run this you can write on your terminal:

```(bash)
dotnet run --project Inoa PETR4 30.2 33.7
```

This takes the first ticker you want to analyze
(and it is mandatory to enter it)
