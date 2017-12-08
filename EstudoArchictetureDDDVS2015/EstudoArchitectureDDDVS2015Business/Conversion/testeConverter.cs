using EstudoArchitectureDDDVS2015DTO;
using EstudoArchitectureDDDVS2015Entity.Database;

namespace EstudoArchitectureDDDVS2015Business.Conversion
{
    public static class testeConverter
    {
        public static testeEntity ParaEntity(testeDTO dto)
        {
            var entity = new testeEntity();

            CopiarPara(dto,entity);

            return entity;
        }

        public static void CopiarPara(testeDTO origem, testeEntity destino, bool existe = true)//boolean para quando necessário
        {
            if (destino == null) return;

            destino.COL1 = origem.COL1;
            destino.COL2 = origem.COL2;
            destino.COL3 = origem.COL3;
        }
    }
}
