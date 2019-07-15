# Order Processor

## How to run the project

This project is developed in `dotnet core` version 2.2.301. One can run the test the project by running following command in the root directory after cloning it


set up sqllite db 

```
cd src/OrderProcessor;
dotnet ef database update;

```


```
dotnet test src/OrderProcessor.Tests
```

To run the sample console app

```
dotnet run --project src/OrderProcessor
```


## To reset 

Delte orders.db in `src/OrderProcessor` directory and run `dotnet ef database update` 


