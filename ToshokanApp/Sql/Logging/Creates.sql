CREATE TABLE Logs
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Url NVARCHAR(MAX),
    RequestBody NVARCHAR(MAX),
    ResponseBody NVARCHAR(MAX),
    CreationDate DATETIME,
    EndDate DATETIME,
    StatusCode INT,
    HttpMethod NVARCHAR(10)
)
