﻿using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class BalanceDTO
    {
        [Required(ErrorMessage = "Please enter Account Number")]
        [RegularExpression("[0-9]{12}",ErrorMessage = "Account Number should be 12 digits long")]
        public int AccountNumber { get; set; }
        [Required(ErrorMessage = "Please enter Account Balance")]
        public double AccountBalance { get; set; }
    }
}
