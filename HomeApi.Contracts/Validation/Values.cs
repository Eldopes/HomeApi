namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-хранилище допустымых значений для валидаторов
    /// </summary>
    public static class Values
    {
        public static string [] ValidRooms = new  []
        {
            "Кухня",
            "Ванная",
            "Гостиная",
            "Туалет"
        };
        
        public static int [] ValidCurrencies = new  []
        {
           120,
           220
        };
    }
}