using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratorEHZ.Models.SKZ
{
    public class RegisterUpdateFormData
    {
        public int Id { get; set; }
        [DisplayName("Дистанционное отключение и включение силовых модулей")]
        public bool DistantPowerControl { get; set; }
        [Required(ErrorMessage = "Не задан выходной ток")]
        [Range(0, 1000, ErrorMessage = "Значение поля выходного тока должно находиться в диапазоне от 0 до 1000.")]
        [DisplayName("Задание выходного тока")]
        public UInt16? SetOutputCurrent { get; set; }
        [Required(ErrorMessage = "Не задан сум. потенциал")]
        [Range(-500, 500, ErrorMessage = "Значение поля суммарного потенциала должно находиться в диапазоне от -500 до 500")]
        [DisplayName("Задание сум. потенциала")]
        public Int16? SetSumPotential { get; set; }
        [Required(ErrorMessage = "Не задан поляр. потенциал")]
        [Range(-500, 500, ErrorMessage = "Значение поля Поляризационного потенциала должно находиться в диапазоне от -500 до 500")]
        [DisplayName("Задание поляр. потенциала")]
        public Int16? SetPolPotential { get; set; }
        [Required(ErrorMessage = "Не задано упрвление режимами стабилизации станции")]
        [Range(0, 2, ErrorMessage = "Значение поля управление режимами стабилизации станции должно находиться в диапазоне от 0 до 2 ")]
        [DisplayName("Управление режимами стабилизации станции")]
        public UInt16? SetStabControlMode { get; set; }
        [DisplayName("Запрос в ручную")]
        public string ADU{ get; set; } //!!!УДАЛИТЬ!!!
    }
}
