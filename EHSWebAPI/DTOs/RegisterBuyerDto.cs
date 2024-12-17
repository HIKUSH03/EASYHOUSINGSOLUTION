using System;

namespace EHSWebAPI.DTOs
{
    public class RegisterBuyerDto
    {

        public string UserName { get; set; }

        public string Password { get; set; }

        public string UserType { get; set; } = "Buyer";

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNo { get; set; }

        public string EmailId { get; set; }
    }
}
