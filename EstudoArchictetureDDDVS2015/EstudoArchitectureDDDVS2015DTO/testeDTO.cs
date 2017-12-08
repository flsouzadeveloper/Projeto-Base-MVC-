using System.Collections.Generic;

namespace EstudoArchitectureDDDVS2015DTO
{
    public class testeDTO
    {
        public int COL1 { get; set; }
        public string COL2 { get; set; }
        public string COL3 { get; set; }
        public IEnumerable<testeDTO> listaTesteDTO { get; set; }
    }
}
