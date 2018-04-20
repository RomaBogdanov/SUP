namespace SupHost.Data
{
    /// <summary>
    /// Данные пользователя для отслеживания таймаута
    /// </summary>
    class UserTimeoutData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Timeout { get; set; }

        public override bool Equals(object obj)
        {
            var userData = obj as UserTimeoutData;
            return userData != null && userData.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
