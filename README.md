# TestRail

This project is my graduation project after completing the course "Automated Testing in C#". The main goal is to demonstrate proficiency in writing UI tests with Selenium WebDriver and API tests using RestSharp, as well as tests using test data from a local database. Developed in C# with NUnit, the project supports parallel test execution and cross-browser testing for robustness and compatibility. The tests are integrated with CI pipelines in GitHub Actions and generate detailed reports using Allure Report, offering clear insights into the test results. 

Below is an overview of the key features and components implemented in this project. 

## Key Features
- Wrappers
- Page Object Pattern
- Steps
- Loadable Component Pattern
- Value Object Pattern
- Cross-browser Testing
- Parallel Test Execution
- Test Reporting in Allure
- NLog
- Running Tests on GitHub Actions

## Project Structure
- **.github/workflows:** Contains GitHub Actions workflow file.
- **BaseEntities:** Contains base classes such as BaseApiTest, BaseTest, BasePage, and others.
- **Connector:** Houses the DbConnector class responsible for managing PostgreSQL database connections locally. This class handles connection creation, opening, and closing using the Npgsql library.
- **Core:** Core functionality related to browser management and WebDriver setup.
	 - **Browser:** Initializes and manages an instance of IWebDriver based on the browser type configured in Configurator. Maximizes the browser window and sets implicit wait timeouts.
	 - **DriverFactory:** Provides methods to instantiate IWebDriver instances for different browsers (Chrome, Firefox, Edge). Configures browser-specific options and capabilities (e.g., incognito mode, headless mode). Uses DriverManager to ensure the correct version of the browser driver is set up.
- **Elements:** Contains classes responsible for interacting with various UI elements across the project, such as Button, Field, Checkbox, and others. Each class encapsulates functionality specific to UI components, offering methods for actions like clicking, entering text, verifying states, and more.
- **Models:** Contains data model classes used throughout the project, such as ProjectModel and MilestoneModel. These classes define the structure and attributes of data entities used in the application. Each model class includes properties annotated with JsonPropertyName attributes for JSON serialization and deserialization.
- **Pages:** Contains classes representing various pages used in UI tests throughout the project. Each page class encapsulates elements and actions specific to that page, providing methods for interacting with UI components such as fields, buttons, and messages. These classes extend from a base page class (BasePage), implementing page-specific methods.
- **Resources:** Contains a file with test data in JSON format, exclusively utilized for API tests within the project.
- **Services:** Contains classes responsible for handling external services and data interactions within the project.
	 - **ApiService:** Manages API interactions by setting up RestClient instances with configurable options. Provides methods for creating GET and POST requests, and extracting response content from API calls.
	 - **DbService:** Facilitates database operations using NpgsqlConnection. Implements methods to retrieve data entities such as projects and milestones from PostgreSQL database tables. Each method executes SQL queries and maps database records to corresponding model classes (ProjectModel, MilestoneModel).
- **Steps:** Here you'll find classes with methods that perform specific actions or steps used in tests, structured for both API and UI testing purposes.
- **Tests:** Contains classes with test methods organized into the following groups:
	 - **API:** Tests for adding, deleting, and updating projects using API commands. Also includes a special class ProjectCleaner with a test method designed to delete all created projects during test runs.
	 - **DB:** Tests for adding projects and milestones using test data from a local database.
	 - **UI:** Tests for user interface functionality, including user login, creation, and deletion of projects and milestones. Precondition steps for testing milestone functionality involve utilizing API steps.
- **Utils:** Contains several utility classes used for various purposes. For instance, classes like AppSettings, Configurator, and DbSettings are employed to retrieve project settings from the appsettings.json file.
- **appsettings.json:** This file contains configuration settings for the project. It specifies settings such as the browser type, timeout values, Base URL, and database connection details.
- **NLog.config:** This file is used for configuring logging in the project. Written in XML format, it specifies how logs are handled, including writing logs to both files and the console.

## Running Tests with GitHub Actions
Tests are configured to run both on a schedule and manually for selected test categories. Upon completion of testing, a report is generated.

Available test categories for execution include:

- **All:** Includes all tests (except tests from Database, ProjectCleaner, and ToFail categories).
- **Smoke:** Essential tests to ensure basic functionality and health of the application.
- **Login:** Tests related to user login functionality.
- **Project:** Tests related specifically to project creation, deletion, and updates.
- **Milestone:** Tests related specifically to milestone creation and deletion.
- **Cleaner:** Runs separately to delete created entities as needed after testing.
- **ToFail:** Test created to demonstrate the ability to attach a screenshot of the application window at the moment of test failure in the Allure Report.

## Test Reports
In this project, test reporting utilizes the Allure.NUnit library. Tests in the results are structured according to Epic – Feature - Story for comprehensive reporting. Various Allure attributes, such as Description, Author, and Severity, were employed to enhance the informative nature of the reports. In case of test failures, a screenshot capturing the moment of failure is attached to the report (a test method with the category ToFail was created to test this functionality).


### Additional Setup

If you want to use my project, you need to perform the following additional actions:

1. Create your account on the website https://secure.testrail.com/customers/testrail/trial/?type=signup& 
2. Change the TestRailBaseURL setting in the appsettings.json file.
3. Add the following variables with your own values to the Windows environment variables: TESTRAIL_USERNAME and TESTRAIL_PASSWORD. Also, create the same Repository secrets on GitHub. 