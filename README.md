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


## Assumptions

- All version parts are either a single character like 'a', 'A' or a number.
- Any minor or build version are reset as per the charcter sequence of any character between 'A'-'Z' is reset to 'A' and any character between 'a'-'z' is reset to 'a'
- 'Major Version' has to be integer, since alphanumeric is cyclic in nature and at somepoint we have to increase the number.


## Test Cases

I have implemented around 17 test cases for individual components.


