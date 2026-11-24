Lucas Moraes Varela

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    
    
        string caminhoArquivo = "provinhaMaisBarbadinhaAinda.txt";
        if (!File.Exists(caminhoArquivo))
        {
            Console.WriteLine($"Erro: O arquivo {caminhoArquivo} não foi encontrado.");
            return;
        }

        string textoCifrado = File.ReadAllText(caminhoArquivo, Encoding.UTF8);

        
        Console.WriteLine("=== CONTEÚDO DO TEXTO CIFRADO ===");
        Console.WriteLine(textoCifrado);
        Console.WriteLine("\n--------------------------------------------------\n");

    
        string textoDecifrado = DecifrarTexto(textoCifrado);

        
        string textoComQuebras = SubstituirHashesPorQuebras(textoDecifrado);

         
        List<string> palindromosEncontrados = new List<string>();
        string textoFinal = ProcessarPalindromosEE(textoComQuebras, out palindromosEncontrados);

        
        Console.WriteLine("=== PALÍNDROMOS ENCONTRADOS ===");
        if (palindromosEncontrados.Count > 0)
        {
            foreach (var p in palindromosEncontrados)
            {
                Console.WriteLine($"- {p}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum palíndromo encontrado.");
        }
        Console.WriteLine("\n--------------------------------------------------\n");

        
        int numCaracteres = textoFinal.Length;
        Console.WriteLine($"Número de caracteres da versão final do texto: {numCaracteres}");

        
        int numPalavras = ContarPalavras(textoFinal);
        Console.WriteLine($"Quantidade de palavras do texto final: {numPalavras}");
        Console.WriteLine("\n--------------------------------------------------\n");

        Console.WriteLine("=== O TEXTO FINAL ===");
        ImprimirTextoComDestaques(textoFinal);
    }

    static string DecifrarTexto(string textoCifrado)
    {
        char[] caracteres = textoCifrado.ToCharArray();
        char[] decifrados = new char[caracteres.Length];

        for (int i = 0; i < caracteres.Length; i++)
        {
            char c = caracteres[i];

            
            int deslocamento1 = ObterDeslocamentoCiclo1(i);
            char c1 = (char)(c - deslocamento1);

            
            int deslocamento2 = ObterDeslocamentoCiclo2(i);
            char cFinal = (char)(c1 - deslocamento2);

            decifrados[i] = cFinal;
        }

        return new string(decifrados);
    }

    static int ObterDeslocamentoCiclo1(int indice)
    {
        if (indice % 17 == 0)
            return 0;
        else if (indice % 3 == 0)
            return 3;
        else
            return 14;
    }

    static int ObterDeslocamentoCiclo2(int indice)
    {
        if (indice % 17 == 0)
            return 0;
        else if (indice % 5 == 0)
            return 12;
        else
            return 16;
    }

     <summary>
     </summary>
    static string SubstituirHashesPorQuebras(string texto)
    {
        return texto.Replace('#', '\n');
    }

      <summary>
      </summary>
    static string ProcessarPalindromosEE(string texto, out List<string> palindromosEncontrados)
    {
        palindromosEncontrados = new List<string>();

      
       
        string[] palavrasSubstituicao = { "lutas", "batalhas", "cria-las" };
        int contadorPalindromos = 0;
        bool primeiraVezE = true;

        var tokens = System.Text.RegularExpressions.Regex.Split(texto, @"(\s+|[.,!?;:\n])");

        for (int i = 0; i < tokens.Length; i++)
        {
            string token = tokens[i];
           
            if (EhPalavra(token))
            {
                string limpa = token.ToLower();
               
                if (EhPalindromo(limpa) && limpa.Length > 1)
                {
                    palindromosEncontrados.Add(token);
                    if (contadorPalindromos < palavrasSubstituicao.Length)
                    {
                       
                        tokens[i] = $"[VERMELHO]{palavrasSubstituicao[contadorPalindromos]}[/VERMELHO]";
                        contadorPalindromos++;
                    }
                }
                else if (primeiraVezE && limpa == "e")
                {
                    
                    primeiraVezE = false;
                }
            }
        }

        return string.Concat(tokens);
    }

    static bool EhPalavra(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return false;
        foreach (char c in s)
        {
            if (!char.IsLetter(c)) return false;
        }
        return true;
    }

    static bool EhPalindromo(string palavra)
    {
        if (string.IsNullOrEmpty(palavra) || palavra.Length <= 1) return false;
        int min = 0;
        int max = palavra.Length - 1;
        while (min < max)
        {
            if (palavra[min] != palavra[max])
                return false;
            min++;
            max--;
        }
        return true;
    }

    static int ContarPalavras(string texto)
    {
        
        string textoLimpo = texto.Replace("[VERMELHO]", "").Replace("[/VERMELHO]", "");
        string[] palavras = textoLimpo.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        return palavras.Length;
    }

    /// <summary>
   
    /// </summary>
    static void ImprimirTextoComDestaques(string texto)
    {
       
        string[] pedacos = texto.Split(new string[] { "[VERMELHO]", "[/VERMELHO]" }, StringSplitOptions.None);

        bool pintar = false;
        foreach (var pedaco in pedacos)
        {
            if (pintar)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(pedaco);
                Console.ResetColor();
            }
            else
            {
                Console.Write(pedaco);
            }
            pintar = !pintar;
        }
        Console.WriteLine();
    }
}
       
