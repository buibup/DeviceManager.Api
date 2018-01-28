﻿using System;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Data.Model;
using DeviceManager.Api.Model;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class DeviceService : IDeviceService
    {
        private readonly IRepository<Device> deviceRepository;
        private readonly IUnitOfWork unitOfWork;

        /// <inheritdoc />
        public DeviceService(
            IRepository<Device> deviceRepository,
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.deviceRepository = deviceRepository;
        }

        /// <inheritdoc />
        public Device GetDeviceById(Guid deviceId)
        {
            return deviceRepository.Get(deviceId);
        }

        /// <inheritdoc />
        public void CreateDevice(DeviceViewModel deviceViewModel)
        {
            // Add new device
            deviceRepository.Add(
                new Device
                {
                    DeviceId = Guid.NewGuid(),
                    DeviceTitle = deviceViewModel.Title
                });

            // Commit changes
            unitOfWork.Commit();
        }

        /// <inheritdoc />
        public void UpdateDevice(string deviceId, DeviceViewModel deviceViewModel)
        {
            // Get device
            Device device = deviceRepository.Get(deviceId);

            // Update device properties
            device.DeviceTitle = deviceViewModel.Title;

            deviceRepository.Update(device);

            // Commit changes
            unitOfWork.Commit();
        }
    }
}