namespace HomeApi.Data.Queries
{
    /// <summary>
    /// Класс для передачи дополнительных параметров при обновлении устройства
    /// </summary>
    public class UpdateDeviceQuery
    {
        public string NewName { get; }

        public UpdateDeviceQuery(string newName = null)
        {
            NewName = newName;
        }
    }
}