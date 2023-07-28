using MyFirstWebAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Domain
{
    public class ErrorLog : EntityBase
    {
        public Guid UserId { get; set; }
        public string JsonRequest { get; set; }
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Any
        /// </summary>
        public string JsonMessage { get; set; }
        /// <summary>
        /// Get, Put, Post, Delete
        /// </summary>
        public string Action { get; set; }
        public string Method { get; set; }
    }
}
