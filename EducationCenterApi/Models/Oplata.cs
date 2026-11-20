using System.ComponentModel.DataAnnotations;

namespace EducationCenterApi.Models
{
        public class Oplata
    {
        public int Id { get; set; }
        public int RoditelId { get; set; }
        public int RebenokId { get; set; }
        public int KolichestvoZanyatiy { get; set; }
        public decimal Summa { get; set; }
        public string Status { get; set; } = "Не оплачено";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class CreateOplataRequest
    {
        [Required(ErrorMessage = "Укажите ID родителя")]
        public int RoditelId { get; set; }

        [Required(ErrorMessage = "Укажите ID ребёнка")]
        public int RebenokId { get; set; }

        [Range(1, 12, ErrorMessage = "Количество занятий от 1 до 12")]
        public int KolichestvoZanyatiy { get; set; }

        [Range(500, 100000, ErrorMessage = "Сумма от 500 до 100000")]
        public decimal Summa { get; set; }
    }
}