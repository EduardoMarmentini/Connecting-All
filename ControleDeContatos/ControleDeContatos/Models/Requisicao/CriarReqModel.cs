﻿using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models.Requisicao
{
    public class CriarReqModel
    {
        [Key]
        public int id_requisicao { get; set; }
        public required int id_usuario { get; set; }
        public required int id_responsavel { get; set; }
        public required int id_cliente { get; set; }
        public required int id_prioridade { get; set; }
        public required string titulo_requisicao { get; set; }
        public required int status { get; set; }
        public required DateTime data_cadastro { get; set; }
        public DateTime data_entrega { get; set; }
        public DateTime? data_conclusao { get; set; }
        public DateTime horas_trabalhadas { get; set; }
        public required string detalhe_ocorrencia {  get; set; } 
    }
}