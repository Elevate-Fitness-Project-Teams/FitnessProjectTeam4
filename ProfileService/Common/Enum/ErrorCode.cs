namespace ProfileService.Common.Enum
{
    public enum ErrorCode
    {
        None = 0,
        Success = 200,
        Created = 201,
        BadRequest = 400,
        Conflict = 409,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
    }
}
