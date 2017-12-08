using EstudoArchictetureDDDVS2015.Models.Genericas;
using EstudoArchitectureDDDVS2015DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EstudoArchictetureDDDVS2015.Models
{
    public class TesteModel : BaseModel
    {
        public int COL1 { get; set; }

        [Display(Name = "Col Nº 2")]
        public string COL2 { get; set; }

        [Display(Name = "Col Nº 3")]
        public string COL3 { get; set; }

        public IEnumerable<testeDTO> listaTesteDTO { get; set; }

        public testeDTO ParaDTO()
        {
            return new testeDTO
            {
                COL1 = COL1,
                COL2 = COL2,
                COL3 = COL3
            };
        }

        public static TesteModel Apartir(testeDTO dto)
        {
            return new TesteModel
            {
                COL1 = dto.COL1,
                COL2 = dto.COL2,
                COL3 = dto.COL3
            };
        }
    }
}