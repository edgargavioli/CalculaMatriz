using System.Runtime.Serialization;

namespace CalculadoraMatriz.Model
{
    public class Matriz
    {
        public List<List<double>> Valores { get; private set; }

        public Matriz(List<List<double>> valores)
        {
           Valores = valores;
        }

        public void MudarPosicao(List<List<double>> matriz)
        {
            for (int linha = 0; linha < matriz.Count; linha++)
            {
                for (int coluna = 0; coluna < matriz[linha].Count; coluna++)
                {
                    Console.WriteLine("Valores[linha][coluna]" + Valores[linha][coluna]+" "+linha+" "+coluna);
                    if (coluna > 0 && matriz[linha][coluna] == 0 && matriz[linha][coluna - 1] == 0)
                    {
                        int para = coluna + 1;
                        double temp = matriz[linha][coluna];
                        matriz[linha][coluna] = matriz[linha][para];
                        matriz[linha][para] = temp;
                    }
                    else if (coluna == 0 && matriz[linha][coluna] == 0)
                    {
                        int para = coluna + 1;
                        double temp = matriz[linha][coluna];
                        matriz[linha][coluna] = matriz[linha][para];
                        matriz[linha][para] = temp;
                    }
                }
            }
        }
        public void CalcTudo(List<List<double>> matriz)
        {
            MudarPosicao(matriz);
            for (int lin = 1; lin < matriz.Count; lin++)
            {
                for (int col = 0; col < lin; col++)
                {
                    if (matriz[lin][col] != 0)
                    {
                        CalcLinha(lin, col, matriz);
                    }
                }
            }
            CalcVariaveis(matriz);
        }

        public void CalcLinha(int linha, int coluna, List<List<double>> matriz)
        {
            double resultadoMmc = Mmc(matriz[linha][coluna], matriz[coluna][coluna]);
            List<List<double>> matrizPcalculo = CriarMatrizAuxiliar(matriz);
            double nl1 = resultadoMmc / matriz[coluna][coluna];
            double nl2 = resultadoMmc / matriz[linha][coluna];
            for (int i = 0; i < matriz.Count + 1; i++)
            {
                matrizPcalculo[coluna][i] *= nl1;
                matriz[linha][i] *= nl2;
            }
            for (int i = 0; i < matriz.Count + 1; i++)
            {
                matriz[linha][i] = matrizPcalculo[coluna][i] - matriz[linha][i];
            }
        }

        public void CalcVariaveis(List<List<double>> matriz)
        {
            string texto = "";
            double[] variaveis = new double[matriz.Count];
            for (int i = 0; i < variaveis.Length; i++)
            {
                variaveis[i] = 1;
            }
            for (int i = 1; i <= matriz.Count; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    matriz[matriz.Count - i][matriz.Count] = matriz[matriz.Count - i][matriz.Count] - matriz[matriz.Count - i][matriz.Count - j] * variaveis[matriz.Count - j];
                    variaveis[matriz.Count - i] = matriz[matriz.Count - i][matriz.Count] / matriz[matriz.Count - i][matriz.Count - i];
                }
            }
            for (int i = 1; i <= variaveis.Length; i++)
            {
                if (i != variaveis.Length)
                {
                    texto += "i" + i + "=" + variaveis[i - 1].ToString("F2") + "; ";
                }
                else
                {
                    texto += "i" + i + "=" + variaveis[i - 1].ToString("F2");
                }
            }
            Console.WriteLine(texto);
        }

        List<List<double>> CriarMatrizAuxiliar(List<List<double>> matriz)
        {
            List<List<double>> matrizAuxiliar = matriz;
            return matrizAuxiliar;
        }

        double Mmc(double valorMatriz1, double valorMatriz2)
        {
            int mmc = 1;
            double maior;
            if (valorMatriz1 < 0)
            {
                valorMatriz1 *= -1;
            }
            if (valorMatriz2 < 0)
            {
                valorMatriz2 *= -1;
            }
            if (valorMatriz1 > valorMatriz2)
            {
                maior = valorMatriz1;
            }
            else
            {
                maior = valorMatriz2;
            }

            for (int i = 1; i <= maior; i++)
            {
                if (valorMatriz1 % i == 0 && valorMatriz2 % i == 0)
                {
                    mmc *= i;
                    valorMatriz1 /= i;
                    valorMatriz2 /= i;
                    i = 1;
                }
                else if (valorMatriz1 % i == 0)
                {
                    mmc *= i;
                    valorMatriz1 /= i;
                    i = 1;
                }
                else if (valorMatriz2 % i == 0)
                {
                    mmc *= i;
                    valorMatriz2 /= i;
                    i = 1;
                }
            }
            return mmc;
        }


    }
}
