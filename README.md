# Baldris Code Examples

The code of Baldris is managed in a single mono-repo which includes front-end, back-end and deployment-scripts (yaml-files for Azure Devops and Kubectl).

The files one can find in the src-directory are taken out context and likely won't compile as much of the code is missing. Included are a example of two controllers for the API (CalendarEvents and Deceased) that uses the Baldris.Application library. Baldris.Application implementation also included.

CalendarEvents includes ceremonies and transport etc.

## Solution overview

Here's a screenshot of some of the services-hierarchy:

![Picture of project overview](/assets/solution-overview.png)

### Baldris service

This is the main service which encompasses most of the functionality.

-   Baldris.Api: REST-ful api that mostly serves as a gateway. Request are redirected to other services (image-processing, signatures, reports) where appropriate. Authentication and Authorization is handled here.
-   Baldris.Application: The main business logic library and the library all consumers dealing with Baldris uses. Built in a CQRS-fashion which separates commands (crud-operations) and queries. Uses IMediatR for commands/queries and AutoMapper + a Entity Framework abstraction for dealing with the database. Contains DTOs that are exposed to the api and other consuming libraries.
-   Baldris.CloudFunctions: Serverless functions that deals with sending EHF-invoices to Evry's multi-channel functionality. Also deals with status-updates from Sendgrid (email-statuses)
-   Baldris.Core: Common helper classes
-   Baldris.CronJobs: (under development) Code that runs on a schedule. Currently being developed to run automatic invoice-journal exports.
-   Baldris.Dal: ORM (Entity Framework) for communicating with the database and maintaining migrations etc.
-   Baldris.Entities: Database models.
-   Baldris.Infrastructure: Code that deals with 3rd-party services and libraries like file storage (Azure storage), email-sending (Sendgrid), PDF-generation (DevExpress) etc.
-   Baldris.Reports: Queries and custom functionality for PDF-generation of reports
-   Baldris.Reports.Server: Not published anywhere, just a server to spin up in development to run DevExpress' report designer

### Other services

-   image-processing: delivers resized and cropped images to the front-end and report-generation
-   midgard: (Under development) Barely started development on a service (customer dashboard) which end-users can log into.
-   nanna: (Under development) Barely started development on a service (partner dashboard) which the funeral homes' partners can log into and receive and update orders (flower orders, headstone order, service sheet orders etc.)
-   shared: Services like digital signature collection, external credentials management
-   tenants: Tenant service that holds the tenants and connection strings for database and file storage

## Database schema example

![Baldris-db-schema](/assets/db-diagram.png)
