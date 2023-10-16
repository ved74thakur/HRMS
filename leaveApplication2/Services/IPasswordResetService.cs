namespace leaveApplication2.Services
{
    public interface IPasswordResetService
    {

        void SendPasswordResetEmail(string email);

    }
}
