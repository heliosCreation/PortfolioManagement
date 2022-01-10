namespace Portfolio.Application.Models.Identity
{
    public class EmailConfirmationModel
    {
        public string Email { get; set; }

        public bool IsSent { get; set; } = false;

        public bool IsVerified { get; set; } = false;

    }
}
