using System;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Data.Repos
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Device" в базе
    /// </summary>
    public class DeviceRepository : IDeviceRepository
    {
        private readonly HomeApiContext _context;
        
        public DeviceRepository (HomeApiContext context)
        {
            _context = context;
        }

        public async Task<Device[]> GetAll()
        {
            return await _context.Devices
                .Include( d => d.Room)
                .ToArrayAsync();
        }

        public async Task<Device> GetDeviceByName(string name)
        {
            return await _context.Devices
                .Include( d => d.Room)
                .Where(d => d.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Device> GetDeviceById(Guid id)
        {
            return await _context.Devices
                .Include( d => d.Room)
                .Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveDevice(Device device, Room room)
        {
            device.RoomId = room.Id;
            device.Room = room;
            
            var entry = _context.Entry(device);
            if (entry.State == EntityState.Detached)
                _context.Devices.Update(device);
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDevice(Device device)
        {
            var entry = _context.Entry(device);
            if (entry.State == EntityState.Detached)
                _context.Devices.Remove(device);
            
            await _context.SaveChangesAsync();
        }
    }
}