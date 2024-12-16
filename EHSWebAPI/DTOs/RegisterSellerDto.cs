using System;

namespace EHSWebAPI.DTOs
{
    public class RegisterSellerDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string UserType { get; set; } = "Seller";

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNo { get; set; }

        public string Address { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public string EmailId { get; set; }

    }
}