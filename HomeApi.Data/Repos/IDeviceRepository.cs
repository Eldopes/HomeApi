using System;
using System.Threading.Tasks;
using HomeApi.Data.Models;

namespace HomeApi.Data.Repos
{
    public interface IDeviceRepository
    {
        Task<Device[]> GetAll();
        Task<Device> GetDeviceByName(string name);
        Task<Device> GetDeviceById(Guid id);
        Task SaveDevice(Device device, Room room);
        Task DeleteDevice(Device device);
    }
}