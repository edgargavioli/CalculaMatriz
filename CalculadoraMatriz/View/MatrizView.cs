using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CalculadoraMatriz.View
{
    public class MatrizConverter : JsonConverter<int[,]>
    {
        public override int[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, int[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            for (int i = 0; i < value.GetLength(0); i++)
            {
                writer.WriteStartArray();

                for (int j = 0; j < value.GetLength(1); j++)
                {
                    writer.WriteNumberValue(value[i, j]);
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }
    }
    public class MatrizView
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        [JsonConverter(typeof(MatrizConverter))]
        public int[,] Valores { get; set; }
        public MatrizView() { }
        public MatrizView(int linhas, int colunas, int[,] valores)
        {
            Linhas = linhas;
            Colunas = colunas;
            Valores = valores;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MatrizView modelo = new MatrizView
            {
                Linhas = 2,
                Colunas = 2,
                Valores = new int[,] { { 1, 2 }, { 3, 4 } }
            };

            string json = JsonSerializer.Serialize(modelo);
            Console.WriteLine(json);
        }
    }

}
