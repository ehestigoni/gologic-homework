# gologic-homework
Virtual Vending Machine test


### Description of the problem and solution
This project provides basic functionality of a Virtual Vending Machine.
The vending machine contains a list of products, with respective prices and quantity available.
Users put in (virtual) money and purchase an item.
After they have purchased an item, they can use the remaining money to purchase another item or have the change returned to them.
Once they are done, they can see a list of the items they have purchased.


### Whether the solution focuses on back-end, front-end or if it's full stack
The solution is focused on the back-end. No UI is provided


### Reasoning behind your technical choices, including architectural
Kept as simple as possible, trying to emphasize code structure and dependency injection to facilitate unit testing.
Used VisualStudio, app hosted on IISExpress.


### Trade-offs you might have made, anything you left out, or what you might do differently if you were to spend additional time on the project.
 - Did not attempt to add a database or proper data storage.
 - Simplified by using a memory cache to keep state - which I am well aware it is NOT fit for production :(
 - The structure I'd like to reach would be to abstract the controller layers into pass-through methods and move all the logic to a service behind.
 - The service would then deal with a real dataSource.
 - Did not attempt to add front end code or any data visualization. Can interrogate API and get json back following samples in default page and BasicIntegrationTests.txt
 - Did not use IDs for users or products. Searches are done by user name and product name instead for simplicity - Not fit for production
 - No authentication/authorization, anyone can manipulate any user account balance/purchases.
 - No logging, no input validation.


### General Notes.
I do not have much experience writing small webApps from scratch nor the technical stack I've chosen. I am aware the solution is not production quality.
It does showcase unit testable modules, and handling of a few edge cases as per functional spec.
If I had not started from scratch (possibly from a skeleton template) I believe I would have done a lot better.
It was a fun challenge (in the given time-frame) and apart from the significant trade-offs I think I got most of the functional specs in.


### Link to your resume or public profile.
https://www.linkedin.com/in/eduardo-estigoni/