using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Revisao_01.Models;

namespace Revisao_01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MegaSenaController : ControllerBase
    {
        

        private readonly string _registro_jogo;

        public MegaSenaController()
        {
            _registro_jogo = Path.Combine(Directory.GetCurrentDirectory(), "Data", "registro_jogo.json");
        }

        private List<RegistrarJogoMegaSena> LerJogosDoArquivo()
        {
            if (!System.IO.File.Exists(_registro_jogo))
            {
                return new List<RegistrarJogoMegaSena>();
            }

            string json = System.IO.File.ReadAllText(_registro_jogo);
            return JsonConvert.DeserializeObject<List<RegistrarJogoMegaSena>>(json);
        }

        private void EscreverJogoNoArquivo(List<RegistrarJogoMegaSena> produtos)
        {
            string json = JsonConvert.SerializeObject(produtos);
            System.IO.File.WriteAllText(_registro_jogo, json);
        }

        [HttpPost]
        public IActionResult Post(RegistrarJogoMegaSena registroJogo)
        {
            if (registroJogo.primeiro_numero != registroJogo.segundo_numero
              && registroJogo.segundo_numero != registroJogo.terceiro_numero
              && registroJogo.terceiro_numero != registroJogo.quarto_numero
              && registroJogo.quarto_numero != registroJogo.quinto_numero
              && registroJogo.quinto_numero != registroJogo.sexto_numero

              )
            {
                var listaJogos = LerJogosDoArquivo();
                listaJogos.Add(registroJogo);
                EscreverJogoNoArquivo(listaJogos);
                return Ok("Jogo Registrado com sucesso!");

            }
            else
            {
                return BadRequest("Existem numeros repetidos, o jogo não pode ser gravado!");

            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(LerJogosDoArquivo());
        }
    }
}
