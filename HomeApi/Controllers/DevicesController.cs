using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Data.Models;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер устройсив
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private IDeviceRepository _devices;
        private IRoomRepository _rooms;
        private IMapper _mapper;
        
        public DevicesController(IDeviceRepository devices, IRoomRepository rooms, IMapper mapper)
        {
            _devices = devices;
            _rooms = rooms;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        [HttpGet] 
        [Route("")] 
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _devices.GetAll();

            var resp = new GetDevicesResponse
            {
                DeviceAmount = devices.Length,
                Devices = _mapper.Map<Device[], DeviceView[]>(devices)
            };
            
            return StatusCode(200, resp);
        }
        
        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost] 
        [Route("Add")] 
        public async Task<IActionResult> Add( AddDeviceRequest request )
        {
            var room = await _rooms.GetRoomByName(request.RoomLocation);
            if(room == null)
                return StatusCode(400, $"Ошибка: Комната {request.RoomLocation} не подключена. Сначала подключите комнату!");
            
            var device = await _devices.GetDeviceByName(request.Name);
            if(device != null)
                return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует.");
            
            var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
            await _devices.SaveDevice(newDevice, room);
            
            return StatusCode(201, $"Устройство {request.Name} добавлена!");
        }
        
        /// <summary>
        /// Обновление существующего устройства
        /// </summary>
        [HttpPatch] 
        [Route("{id}/Edit")] 
        public async Task<IActionResult> Edit(
            [FromRoute] Guid id,
            [FromBody]  EditDeviceRequest request)
        {
            throw new NotImplementedException();

            //   var device = await _devices.GetDeviceById(id);

            // TODO:  дописать 
        }
    }
}