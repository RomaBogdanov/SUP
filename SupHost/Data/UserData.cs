namespace SupHost.Data
{
    /// <summary>
    /// Данные пользователя для отслеживания таймаута
    /// </summary>
    class UserData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var userData = obj as UserData;
            return userData != null && userData.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
