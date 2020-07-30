# Change Log

## [Part 3] - Modify the existing code to ensure that any orders that would have been processed during the weekend resume processing on Monday.
- Added Moonpig.PostOffice.Behavour.Tests project containing use cases stated within test.  You will require the [SpecFlow for Visual Studio 2019](https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowForVisualStudio) visual studio extension to use this project.
- Implemented DespatchCalculator to enable development of new change with isolated test cases
- Updated dependency injection to inject DespatchCalculator into DespatchDateController
- Updated DespatchController to use DespatchCalculator and added further perfromance enhancements

## [Parts 1 & 2] - Refactor existing code
- Moonpig.PostOffice.Data
-- DbContext
--- Renamed to DespatchDbContext.  This avoids the class having the same name as entity frameworks DbContext.
--- Updated all properties to be a expression-bodied properties.
--- Fixed typos in test data (T-Shrts -> T-Shirts, Thshrt -> T-Shirt
-- IDbContext
--- Renamed to IDespatchDbContext.  To align with the implementation name.

- Moonpig.PostOffice.Api
-- Program
--- Removed unncessary using statements

-- Startup
--- Added IDbContext service to services, resolving an instance of DbContext.  This is ok for the purpose of this test and can later be changed to use an entity framework database context
--- Removed unnecessary using statements.
--- Removed comments.  We should all know what these methods are used for.

-- Despatch Date Controller
--- Moved _mlt variable to within Get method.  This variable is only ever used within the Get method.
--- Renamed _mlt to _maxLeadTime.  Abreviations and acronyms should not be used to make code easily understandable to other people.
--- Removed comment.  No need to specify the meaning due to renaming _mlt
--- Changed ID variable name to id.  Incorrect case.
--- Injected IDespatchDbContext through the constructor and created a readonly local variable to host this object.  
--- Refactored code to use injected instance of IDespatchDbContext
--- Renamed s variable to supplierId.  Abreviations and acronyms should not be used to make code easily understandable to other people.
--- Renamed lt variable to leadTime.  Abreviations and acronyms should not be used to make code easily understandable to other people.
--- Added blank line after foreach statement.  There should always be a blank line after curly braces unless followed by another curly bace to make reading the code easy on the eye.
--- Replaced final if..else if.. else statement with a switch statement.  This makes the code easier to follow.
--- Added curly braces around if statement body.  Makes code uniform, improving readability.
--- Reordered using statements.  Using statements should always be in alpha-numeric order.
--- Added supplier not found exception
--- Added exceptions for supplier not found and product not found.  Custom exceptions should aways be thrown when a business error occurs.
--- Adding days to order date is performed 2 times if the max lead time condition is met, introduced a variable to hold the current lead time.  Performance issue

- Moonpig.PostOffice.Tests
-- PostOfficeTests
-- Renamed to DespatchDatecontrollerTests
-- Refactored to use fixture and base class.  Enables tests to be written using when...should naming convention, making it easier to identify the specific cause of failure if/when tests fail
-- Refactored to use specific dates. DateTime.Now was used which could cause some test to fail when on some days of the week.
-- Added test to throw ProductNotFoundException
-- Added test to throw SupplierNotFoundException
-- Renamed Moonpig.PostOffice.Tests to Moonpig.PostOffice.Unit.Tests

- Added test and src folders to solution and disk structure

