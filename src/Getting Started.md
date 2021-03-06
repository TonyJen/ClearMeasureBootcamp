## Getting Started

Clear Measure Bootcamp Projct Preqsets

To work on the Clear Measure Expense Report project you should have a basic understanding of the following prerequisite:

Basic knowledge of Visual Studio, C#, MVC, SQL, FluentNHibernate, Selenium, Onion Architecture, 
Unit testing, Razor Views, Jquery, Bootstrap.

So lets get started:

## Project Overview

The bootcamp project have 4 major sections:

1. Core section - This section contains core project which contains the project's objects 
and services.

2. Data section - This section contains the DataAccess and Database projects.

3. UI section - This section contains UI.DependencyResolution and UI projects.

4. Test section - This section contains UnitTests, IntegrationTests, PerformanceTests, and SmokeTests.

Lets look at the projects individually.

1. Core Project - Contains the models, features, plugins and services. 

2. DataAccess Project - Contains data access functions and well as database transaction 
functions.

3. Database Project - Allows the user to reset the database to factory defaults and 
start the project from a base line.

4. IntregrationTests Project - Contains test data and tests of certain core and 
database functionalities.

5. PerformanceTests Project - Start project performance tests on the Azure platform.

6. SmokeTests Project- Quick test of the integrity of the project using Selenium on
various browsers.

7. UI Project - The user interface of the project. Contains applicaiton pages and UI
functionality.

8. UI.DependencyResolution Project - Contains UI depencency of the project.

9. UnitTests Project - Contains test suites of the Core project. 

Let me just talk briefly about application development in general. The main point of 
breaking things down to individual parts is to help the user reason about the 
application in a better way. In application development, the structure needs to be 
organized and be consistent because the whole is very complex and difficult to understand. 
If that is not done then chaos and inefficiency will very quickly seep into the project 
and derail any progress.

Now we can dive deeper into each project and describe some main concepts.

## Core Project

Core project consists of 5 main parts:

1. Features 
2. Model
3. Plugins
4. Service
5. Bus, IRequest, IRequestHandler

We can discuss them in detail.

**Features**

Expense Report features contains 3 main areas:

1. MulitpleExpenses
2. SearchExpenseReports
3. Workflow

**MulitpleExpenses**

MulitpleExpenses features AddExpenseCommand and AddexpenseResult.

AddexpenseCommand is a request object that takes in Report, Currentuser, Amount, 
Description and CurrentData.

The result is of type AddExpenseResult.

**SearchExpenseReports**

SearchExpenseReports feature contains ExpenseReportSpecificationQuery which is a 
request. SearchReportSpeicificaitonQuery contains a request which takes Status, Approver, Submitter.

**Workflow**

Workflow contains 3 files:

1. ExecuteTransitionCommand
2. ExecuteTransitionCommandHandler
3. ExecuteTransitionResult

ExecuteTransitionCommand is a request that takes in Report, command, currentUser, currentDate. 

ExecuteTransitionCommandHandler is a request handler that uses ExecuteTransitionCommand, and 
saves expense report and returns ExecuteTransitionResult result.

ExecuteTransitionResult is the result that contains NewStatus, NextStep, Action, Message.

**Model**

The next section of the Core project is the Model. This is the most straight forward piece 
of the project. Model contains objects that are used in other piece of the core and the solution.
It have very little logic, and mostly contains classes, constructors and preset values 
like in ExpenseReportFact, ExpenseReportStatus, and AuditEntry. 

The one thing that I like to touch on is in ExpenseReportWorkflow folder. There is a 
StateCommandBase and there are a few objects that derive from the base command. The base command 
implements the IStateCommand interface.


**Plugins / DataAccess**

Pluging area contains a DataAccess folder. The contents are models of TRequest type such as 
EmployeeByUserNameQuery, EmployeeSpecificationQuery, ExpenseReportByNumberQuery, ExpenseReportSaveCommand.
It also contains SingleResult and MulipleResult is of TResponse type.

**Services**

Services are functions that uses models to create function the application uses. Each funciton uses
an interface and implements them. For example, IApplicationInformation is an interface and 
ApplicationInformation implements that functionality. Another example is IExpenseReportBuilder, 
which is implemented by ExpenseReportBuilder.

**Bus Architecture**

Bus architecture is the heart of how requests are done on the Expense Report application. The 
architecture have 3 main parts, Bus, IRequest and IRequestHandler. 

The main method to send request is the send method layed out as follows.

```C#

  /**
         * Send request wrapped in IRequest interface. TResponse is the return value for this method as well as 
         * IRequest interface.
        */
        public virtual TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var defaultHandler = GetHandler(request);

            TResponse result = defaultHandler.Handle(request);

            return result;
        }

```

After the request is send, the next piece of code will find the handler for the request 
and run the corresponding handler method.

```C#

 private RequestHandler<TResponse> GetHandler<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var wrapperType = typeof(RequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            object handler;
            try
            {
                handler = _singleInstanceFactory(handlerType);

                if (handler == null)
                    throw new InvalidOperationException("Handler was not found for request of type " + request.GetType());
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Handler was not found for request of type " + request.GetType(), e);
            }
            var wrapperHandler = Activator.CreateInstance(wrapperType, handler);
            return (RequestHandler<TResponse>)wrapperHandler;
        }

private class RequestHandler<TCommand, TResult> : RequestHandler<TResult> where TCommand : IRequest<TResult>
        {
            private readonly IRequestHandler<TCommand, TResult> _inner;

            public RequestHandler(IRequestHandler<TCommand, TResult> inner)
            {
                _inner = inner;
            }

            public override TResult Handle(IRequest<TResult> message)
            {
                return _inner.Handle((TCommand)message);
            }
        }

```

## Data area

This is where database operation and structure is created.

## DataAccess

DataAccess is where data mapping and data queries are done. 

The examples given are EmployeeMap, ExpenseReportFactMap, ExpenseReportMap, 
ExpenseReportStatusType, ManagerMap.

**Handler**

There is where the Bus handler code that deals with database access is stored. 
Function are furthered encapsulated. One example is AddExpenseCommadnHandler 
which create and updated model and then calls ExpenseReportSaveCommandHandler
which saves the model to the database.

## Database

Database contains scripts which will clear the database of tables and restart 
the database from a base point. 


## UI

This area contains the user interface of the project.

## UI

The UI is organized as a MVC application using Jquery and Bootstrap. 
There are Account, ExpenseReport, ExpenseReportSearch and Home views. 

Likewise the corresponding controllers contains the server based code that support 
the view and it's operations.

## UI.DependencyResolution


## Test 

This is the area of various tests.

## IntegrationTests

Once database is cleared, integration test will create dummy data and run tests 
to make sure the application is working
properly.

## Performance Tests

## SmokeTests

## UnitTests

Units tests are automatic test that test application functionality, usually at the method level.
We are testing the core project objects. We are testing Model and Services areas. 

Here is an example of a basic test:


```C#

  [Test]
        public void PropertiesShouldInitializeProperly()
        {
            var employee = new Employee();
            Assert.That(employee.Id, Is.EqualTo(Guid.Empty));
            Assert.That(employee.UserName, Is.EqualTo(null));
            Assert.That(employee.FirstName, Is.EqualTo(null));
            Assert.That(employee.LastName, Is.EqualTo(null));
            Assert.That(employee.EmailAddress, Is.EqualTo(null));
        }


Here, we test if a Employee object is created successfully and assert if it's values are valid.
Each object in the Core project can be tested that way.






