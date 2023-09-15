using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetLife.Models
{
    public class Animal
    {
        [Key]
        public long? idAnimal { get; set; }

        [Display(Name = "Nome do Pet")]
        public string nomeAnimal { get; set; }

        [Display(Name = "Raça")]
        public string racaAnimal { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public string dataNascAnimal { get; set; }

        [Display(Name = "Peso")]
        public double pesoAnimal { get; set; }

        [Display(Name = "Tipo")]
        public  string tipoAnimal { get; set; }

        [Display(Name = "Características")]
        public string caracteristicasAnimal { get; set; }

       public string? FotoMimeType { get; set; }
       public byte[]? fotoAnimal { get; set; }

       [NotMapped]
       public IFormFile? formFile { get; set; }


        public virtual ICollection<Vacina> Vacinas { get; set; }
        public virtual ICollection<Medicamento> Medicamentos { get; set; }
    }
}
