# ticketfinder

Ticket Finder is a hobby project that helps users find tickets for various events such as concerts, cinema screenings, sports events, and more.

**Features**
Find tickets for concerts, cinema screenings, sports events, and more.
Customer endpoint for managing customer-related operations.
Event Detail endpoint for managing event-related operations.
More endpoints and APIs are being developed and will be available soon.
Technologies Used
Backend: C#, ASP.NET Core
Database: SQL Server
Frontend: React (Not implemented yet)

**Getting Started**
**Prerequisites**
.NET Core SDK installed
SQL Server installed
Installation
Clone the repository:
shell
Copy code
git clone https://github.com/mystafe/ticket-finder.git
Navigate to the project directory:
shell
Copy code
cd ticket-finder
Update the database connection string in the appsettings.json file.

Run the database migrations to create the necessary tables:

shell
Copy code
dotnet ef database update
Build and run the application:
shell
Copy code
dotnet run
The application will start running on http://localhost:7169.

**API Documentation**
Endpoints are mostly consructed:

https://www.postman.com/mystafe/workspace/mypublicworkspace/collection/26765413-97a76028-36e3-4dd7-b84f-ef1daebc1b97

https://documenter.getpostman.com/view/26765413/2s93z5Ajso

![image](https://github.com/mystafe/ticketfinder/assets/75567558/dedd738f-5aa3-4c27-af0a-8048302a86ab)

**_Customer Endpoints:_**
![image](https://github.com/mystafe/ticketfinder/assets/75567558/ddd1ff7c-6a44-4c38-9f6b-a26dfba8629a)

**_EventDetail Endpoints:_**
![image](https://github.com/mystafe/ticketfinder/assets/75567558/ab55f744-d985-49f0-b34d-02365ece85f2)

**_Address Endpoints_**
GET​/api​/Address
POST​/api​/Address
GET​/api​/Address​/{id}

**_City Endpoints_**
GET​/api​/City
POST​/api​/City
GET​/api​/City​/{id}

**_Country Endpoints_**
GET​/api​/Country
POST​/api​/Country
GET​/api​/Country​/{id}

**_Customer Endpoints_**
GET​/api​/Customer
POST​/api​/Customer
GET​/api​/Customer​/{id}

**_Event Endpoints_**
GET​/api​/Event
POST​/api​/Event
GET​/api​/Event​/{id}

**_EventImage Endpoints_**
GET​/api​/EventImage
POST​/api​/EventImage
GET​/api​/EventImage​/{id}

**_EventSeat Endpoints_**
GET​/api​/EventSeat
GET​/api​/EventSeat​/{id}

**_EventStage Endpoints_**
GET​/api​/EventStage
POST​/api​/EventStage
GET​/api​/EventStage​/{id}

**_Place Endpoints_**
GET​/api​/Place
POST​/api​/Place
GET​/api​/Place​/{id}

**_Rating Endpoints_**
GET​/api​/Rating
POST​/api​/Rating
GET​/api​/Rating​/{id}

**_Stage Endpoints_**
GET​/api​/Stage
POST​/api​/Stage
GET​/api​/Stage​/{id}

**_Ticket Endpoints_**
GET​/api​/Ticket
POST​/api​/Ticket
GET​/api​/Ticket​/{id}


**Contributing**
Contributions to the Ticket Finder application are welcome! If you find any bugs or have suggestions for new features, please open an issue or submit a pull request.

**License**
No License

**Contact**
For any questions or inquiries, please contact admess34@gmail.com (Mustafa Evleksiz)
