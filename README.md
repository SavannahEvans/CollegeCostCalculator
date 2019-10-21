# College Cost Calculator

Created by Savannah Evans, evanss@msoe.edu.

## Introduction

Welcome to the college cost calulator. This calculator will provide tuition and room & board costs for 489 colleges in the US.

## Usage

To recieve basic information about a college use the sample command line application or create your own application to consume the CostCalculator. Please note: The CostCalculator.GetCost method will only return results for a college with an exact matching name to one in the data.

### Command Line

Given you have .NET Core installed, open a command prompt in the CollegeCostCalculator directory and run:

```console
> dotnet run <college name>
> dotnet run <college name> <include room & board>

> dotnet run "University of Wisconsin, Madison"
$22,231.00

> dotnet run "University of Wisconsin, Madison" False
$10,955.00
```

### Custom Application

To use the CostCalculator in your own custom application include a package reference to the project in your .csproj file:

```csharp
<ItemGroup>
    <ProjectReference Include="..\CollegeCostCalculator\CollegeCostCalculator.csproj" />
</ItemGroup>
```

The following are examples of how to use the CostCalculator:

```csharp
CostCalculator calculator = new CostCalculator();

// Get cost of tuition + room and board
var cost = calculator.GetCost("University of Wisconsin, Madison");

// Get cost of tuition only
var cost2 = calculator.GetCost("University of Wisconsin, Madison", false);
```

## Tests

To run the tests, given you have .NET Core installed, open a command prompt in the CollegeCostCalculator.Tests directory and run:

```console
dotnet test
```

## Future improvements

Given more time to develop this application I would include the following:

* Ability to match partial college names.
* Ability to request the costs for a list of colleges.
* Ability to retrieve all availible colleges, or all partially matching a given string.
* Ability to specify in-state or out-of-state tuition.
  * Option 1: Add a boolean flag to the GetCost method.
  * Option 2: Create a seperate method for getting each value.
* Ability to retrieve just the cost of room and board.
* More tests and a dedicated csv and json test files with all edge cases.
