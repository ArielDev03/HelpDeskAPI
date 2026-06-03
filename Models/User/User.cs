namespace HelpDeskAPI.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? Edad { get; set; } //opcional
        public string Direccion { get; set; } = string.Empty;
        public string? Ciudad { get; set; } //opcional



    }
}
