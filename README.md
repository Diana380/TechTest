## Changelog

* implemented **Active Only** and **Non Active** filter buttons
* added `DateOfBirth` property to the `User` class which is to be used and displayed in relevant sections of the app.
* added  **Add** screen that allows you to create a new user and return to the list
* added **View** screen that displays the audit logs for that user, i.e. any changes to the user properties
* added **Edit** screen that allows you to edit a selected user from the list
* added **Delete** screen that allows you to perform a hard delete of a selected user from the list, the audit logs for that user are kept and will be displayed in the user audit logs
* implemented audit log on user actions, i.e. create, update, delete. The logs display in the Audit User Logs razor component. As mentioned above, the each user's audit logs can also be accessed in the View action in the UserList component

## architecture changes:

* Updated the data access layer to use a SQL database and implemented database schema migrations. Installed EF
* Updated the data access layer to support asynchronous operations.
* refactored the existing Razor Web application into two components: a .net core API and a Blazor frontend application.
* updated existing tests to match the changed architecture and added audit log repository tests
