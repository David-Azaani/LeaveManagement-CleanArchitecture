﻿using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Contracts.Logging;
using LeaveManagement.Application.Contracts.Models.Email;
using LeaveManagement.Application.Contracts.Persistence;
using LeaveManagement.Application.Exceptions;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _appLogger;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender,
        IAppLogger<CancelLeaveRequestCommandHandler> appLogger)
    {
        this._leaveRequestRepository = leaveRequestRepository;
        this._emailSender = emailSender;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(leaveRequest), request.Id);

        leaveRequest.Cancelled = true;

        // if already approved, re-evaluate the employee's allocations for the leave type

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                       $"has been cancelled successfully.",
                Subject = "Leave Request Cancelled"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _appLogger.LogWarning(ex.Message);
        }
        return Unit.Value;
    }
}

