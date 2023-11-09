namespace leaveApplication2.Dtos
{
    public record LoginDetailDto(long EmployeeId,int RoleAssignId, string UserName, string Password, string firtsName, string lastName, string Token = "", string refreshToken="", DateTime RefreshTokenExpiryTime = default);
    public record LoginInfo(string emailAdderss, string employeePassword);
}
