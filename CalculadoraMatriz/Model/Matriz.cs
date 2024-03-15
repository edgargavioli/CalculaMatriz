using System.Runtime.Serialization;

namespace CalculadoraMatriz.Model
{
    public class Matriz
    {
        public int Linhas { get; private set; }
        public int Colunas { get; private set; }
        [DataMember]
        public int[,] Valores { get; private set; }

        public Matriz(int linhas, int colunas, int[,] valores)
        {
            Linhas = linhas;
            Colunas = colunas;
            Valores = new int[linhas, colunas];
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    Valores[i, j] = valores[i, j];
                }
            }
        }

        public void MudarPosicao(int[,] matriz)
        {
            for (int linha = 0; linha < matriz.Length; linha++)
            {
                for (int coluna = 0; coluna < matriz.Length; coluna++)
                {
                    if (coluna > 0 && matriz[linha, coluna] == 0 && matriz[linha, coluna - 1] == 0)
                    {
                        int para = coluna + 1;
                        int temp = matriz[linha, coluna];
                        matriz[linha, coluna] = matriz[linha, para];
                        matriz[linha, para] = temp;
                    }
                    else if (coluna == 0 && matriz[linha, coluna] == 0)
                    {
                        int para = coluna + 1;
                        int temp = matriz[linha, coluna];
                        matriz[linha, coluna] = matriz[linha, para];
                        matriz[linha, para] = temp;
                    }
                }
            }
        }
        public void CalcTudo(int[,] matriz)
        {
            MudarPosicao(matriz);
            for (int lin = 1; lin < matriz.GetLength(0); lin++)
            {
                for (int col = 0; col < lin; col++)
                {
                    if (matriz[lin, col] != 0)
                    {
                        CalcLinha(lin, col, matriz);
                    }
                }
            }
            CalcVariaveis(matriz);
        }

        public void CalcLinha(int linha, int coluna, int[,] matriz)
        {
            int resultadoMmc = Mmc(matriz[linha, coluna], matriz[coluna, coluna]);
            int[,] matrizPcalculo = CriarMatrizAuxiliar(matriz);
            int nl1 = resultadoMmc / matriz[coluna, coluna];
            int nl2 = resultadoMmc / matriz[linha, coluna];
            for (int i = 0; i < matriz.GetLength(1) + 1; i++)
            {
                matrizPcalculo[coluna, i] *= nl1;
                matriz[linha, i] *= nl2;
            }
            for (int i = 0; i < matriz.GetLength(1) + 1; i++)
            {
                matriz[linha, i] = matrizPcalculo[coluna, i] - matriz[linha, i];
            }
        }

        double[,] CalcVariaveis(int[,] matriz)
        {
            string texto = "";
            double[] variaveis = new double[matriz.GetLength(0)];
            double[,] matrixAux = CriarMatrizAuxiliarDouble(matriz);
            for (int i = 0; i < variaveis.Length; i++)
                variaveis[i] = 1;
            for (int i = 1; i <= matriz.GetLength(0); i++)
            {
                for (int j = 1; j < i; j++)
                    matrixAux[matriz.GetLength(0) - i, matriz.GetLength(0)] -= matriz[matriz.GetLength(0) - i, matriz.GetLength(0) - j] * variaveis[matriz.GetLength(0) - j];
                    variaveis[matriz.GetLength(0) - i] = matriz[matriz.GetLength(0) - i, matriz.GetLength(0)] / matriz[matriz.GetLength(0) - i, matriz.GetLength(0) - i];
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
            return matrixAux;
        }

        int[,] CriarMatrizAuxiliar(int[,] matriz)
        {
            int[,] matrizAuxiliar = new int[matriz.GetLength(0), matriz.GetLength(1)];
            for (int linha = 0; linha < matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                {
                    matrizAuxiliar[linha, coluna] = matriz[linha, coluna];
                }
            }
            return matrizAuxiliar;
        }

        double[,] CriarMatrizAuxiliarDouble(int[,] matriz)
        {
            double[,] matrizAuxiliar = new double[matriz.GetLength(0), matriz.GetLength(1)];
            for (int linha = 0; linha < matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                {
                    matrizAuxiliar[linha, coluna] = matriz[linha, coluna];
                }
            }
            return matrizAuxiliar;
        }

        int Mmc(int valorMatriz1, int valorMatriz2)
        {
            int mmc = 1;
            int maior;
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
