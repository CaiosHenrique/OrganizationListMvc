using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationListMvc.Models
{
    public class TarefaViewModel
    {  
        [Key]
        [Column ("IdTarefa")]
        public int IdTarefa {get; set;} 

        public string? DescTarefa {get; set;} 
        
        public bool StatusTarefa {get; set;} = false;

        public DateTime DataCriacao {get; set;}

        public string NomeTarefa {get; set;} = "Tarefa";

        public DateTime Data() 
        {
            DataCriacao = DateTime.Now;
            return DataCriacao;
        }

    }
    
}