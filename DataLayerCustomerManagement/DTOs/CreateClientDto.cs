﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerCustomerManagement.DTOs
{
    public class CreateClientDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }
    }
}
