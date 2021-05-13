﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Teachify.Models
{
    public partial class ProviderVM
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Education { get; set; }
        public string Language { get; set; }
        public string OneLineTitle { get; set; }
        public string Description { get; set; }
        public string Experience { get; set; }
        public string HourlyRate { get; set; }
        public string CourseDomain { get; set; }
        public string City { get; set; }
        public byte[] ImageArray { get; set; }
        public string ImageUrl { get; set; }
        public string LogoUrl
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return string.Empty;
                }
                return String.Format("https://teachify.azurewebsites.net/{0}", ImageUrl.Substring(1));
            }
        }
    }
}