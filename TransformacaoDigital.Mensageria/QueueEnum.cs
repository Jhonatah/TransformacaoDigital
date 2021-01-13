using System;

namespace TransformacaoDigital.Mensageria
{
    public enum QueueEnum
    {
        ConsultoriasAssessorias,
        Normas,
        ProcessosInternos,
        IntegracaoERPProcessosInternos
    }

    public static class EnumExtension
    {
        public static string GetName(this QueueEnum queueEnum)
            => Enum.GetName(typeof(QueueEnum), queueEnum);
    }
}