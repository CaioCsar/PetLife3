using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetLife.Models
{
    public class Vacina
    {
        [Key]
        public long? idVacina { get; set; }

        [Display(Name = "Vacina")]
        public string nomeVacina { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Aplicação")]
        public DateTime dataAplicacaoVacina { get; set; }

        [Display(Name = "Local da Aplicação")]
        public string localAplicacaoVacina { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data da próxima dose")]
        public DateTime dataProximaVacina { get; set; }

        [Display(Name = "Observação")]
        public string observacaoVacina { get; set; }

        [Display(Name = "Foto do Certificado de Vacinação")]
        public byte[] fotoCertificadoVacina { get; set; }
        public string FotoMimeType { get; set; }
        [NotMapped]
        public IFormFile formFile { get; set; }

        public long? idAnimal { get; set; }
        public Animal Animal { get; set; }
    }
}
