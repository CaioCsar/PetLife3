using System;
using PetLife.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetLife.Models
{
    public class Medicamento
    {
        [Key]
        public long? idMed { get; set; }

        [Display(Name = "Nome do Medicamento")]
        public string nomeMed { get; set; }

        [Display(Name = "Tipo")]
        public string tipoMed { get; set; }

        [Display(Name = "Data de Inicio do Uso")]
        [DataType(DataType.Date)]
        public DateTime dataInicioMed { get; set; }

        [Display(Name = "Dosagem")]
        public int dosagemMed { get; set; }

        [Display(Name = "Período de Uso")]
        public int periodoUsoMed { get; set; }

        [Display(Name = "Intervalo de Uso")]
        public int intervaloUsoMed { get; set; }

        [Display(Name = "Observações")]
        public string observacoesMed { get; set; }

        public long? idAnimal { get; set; }
        public Animal Animal { get; set; }
    }
}
