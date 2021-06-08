using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IntegratorEHZ.Models
{
    public class Device
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Поле \"IMEI\" обязательно для заполнение")]
        public string IMEI { get; set; }
        [DisplayName("Внутренний протокол")]
        [Required(ErrorMessage = "Поле \"Внутренний протокол\" обязательно для заполнение")]
        public string Internal_Protocol { get; set; }

        [JsonIgnore]
        [IgnoreDataMember] 
        public List<DataSKZ> dataSKZ { get; set; }

    }
}
