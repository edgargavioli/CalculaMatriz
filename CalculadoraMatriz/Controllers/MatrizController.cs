using CalculadoraMatriz.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraMatriz.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class MatrizController : ControllerBase
    {
        [HttpPost]
        public IActionResult Calcula([FromBody] Matriz matriz)
        {
            if (matriz == null)
            {
                return BadRequest("Payload inválido");
            }

            try
            {
                matriz.MudarPosicao(matriz.Valores);
                matriz.CalcTudo(matriz.Valores);
                return Ok(matriz); // Retorna um objeto Matriz como resposta
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao calcular: {ex.Message}");
            }
        }
    }
}
