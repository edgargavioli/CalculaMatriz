using CalculadoraMatriz.Model;
using CalculadoraMatriz.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraMatriz.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class MatrizController : ControllerBase
    {
        [HttpPost]
        public IActionResult Calcula([FromBody] MatrizView matrizView)
        {
            if (matrizView == null)
            {
                return BadRequest("Payload inválido");
            }

            Matriz matriz = new Matriz(matrizView.Linhas, matrizView.Colunas, matrizView.Valores);

            try
            {
                matriz.MudarPosicao(matrizView.Valores);
                matriz.CalcTudo(matrizView.Valores);
                return Ok(matriz); // Retorna um objeto Matriz como resposta
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao calcular: {ex.Message}");
            }
        }
    }
}
