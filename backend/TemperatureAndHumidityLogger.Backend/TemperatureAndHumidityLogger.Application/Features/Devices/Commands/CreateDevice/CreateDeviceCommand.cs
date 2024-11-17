﻿using MediatR;
using System;
using TemperatureAndHumidityLogger.Core.Entities.Devices;
using TemperatureAndHumidityLogger.Core.Responses;

namespace TemperatureAndHumidityLogger.Application.Features.Devices.Commands.CreateDevice
{
    public class CreateDeviceCommand : IRequest<WrapResponse<Guid>>
    {
        public Guid SerialNumber { get; set; }
    }
}
