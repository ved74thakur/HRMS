﻿using leaveApplication2.Models;

namespace leaveApplication2.Services
{
    public interface IEmailService
    {
        Task SendLeaveApprovalEmail(AppliedLeave newAppliedLeave);
    }
}
