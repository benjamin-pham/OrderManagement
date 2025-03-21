IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'GetOrders_Pagination'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.GetOrders_Pagination
GO
CREATE PROCEDURE dbo.GetOrders_Pagination
@PageIndex INT = 1,
@PageSize INT = 20,
@SearchTerm NVARCHAR(255) = NULL,
@Status INT = NULL,
@FromCreatedAt DATETIME2 = NULL,
@ToCreatedAt DATETIME2 = NULL,
@FromTotalAmount DECIMAL = NULL,
@ToTotalAmount DECIMAL = NULL
AS
BEGIN
    -- total results
    SELECT COUNT(1)
    FROM Orders AS o
    WHERE (@SearchTerm IS NULL OR @SearchTerm = ''
            OR o.CustomerName LIKE N'%'+@SearchTerm+'%')
        AND (@Status IS NULL OR o.[Status] = @Status)
        AND (@FromCreatedAt IS NULL OR o.CreatedAt >= @FromCreatedAt)
        AND (@ToCreatedAt IS NULL OR o.CreatedAt <= @ToCreatedAt)
        AND (@FromTotalAmount IS NULL OR o.TotalAmount >= @FromTotalAmount)
        AND (@ToTotalAmount IS NULL OR o.TotalAmount <= @ToTotalAmount)

    -- results
    ;WITH pg AS (
        SELECT o.Id
        FROM Orders AS o
        WHERE (@SearchTerm IS NULL OR @SearchTerm = ''
            OR o.CustomerName LIKE N'%'+@SearchTerm+'%')
        AND (@Status IS NULL OR o.[Status] = @Status)
        AND (@FromCreatedAt IS NULL OR o.CreatedAt >= @FromCreatedAt)
        AND (@ToCreatedAt IS NULL OR o.CreatedAt <= @ToCreatedAt)
        AND (@FromTotalAmount IS NULL OR o.TotalAmount >= @FromTotalAmount)
        AND (@ToTotalAmount IS NULL OR o.TotalAmount <= @ToTotalAmount)
        ORDER By o.CreatedAt
        OFFSET (@PageIndex - 1) * @PageSize ROWS 
        FETCH NEXT @PageSize ROWS ONLY
    )
    SELECT
    o.Id,
    o.CustomerName,
    o.[Status],
    o.TotalAmount,
    o.CreatedAt,
    o.UpdatedAt
    FROM pg
    LEFT JOIN Orders AS o ON o.Id = pg.Id
END
GO