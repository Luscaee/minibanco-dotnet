public static class ConsoleHelper
{
    public static int LerInt(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int numero))
                return numero;

            Console.WriteLine("Digite um número inteiro válido.");
        }
    }

    public static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine();

            if (decimal.TryParse(entrada, out decimal numero))
                return numero;

            Console.WriteLine("Digite um valor decimal válido.");
        }
    }

    public static double LerDouble(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? entrada = Console.ReadLine();

            if (double.TryParse(entrada, out double numero))
                return numero;

            Console.WriteLine("Digite um número válido.");
        }
    }

    public static string LerTexto(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? texto = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(texto))
                return texto;

            Console.WriteLine("A entrada não pode ser vazia.");
        }
    }
}
