using System.Globalization;
using System.Text;

CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
Console.OutputEncoding = Encoding.UTF8;

BancoService banco = new BancoService();
MenuPrincipal menu = new MenuPrincipal(banco);

TelaCarregamento.Exibir();
menu.Executar();
