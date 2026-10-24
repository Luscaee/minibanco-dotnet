using System.Text;

Console.OutputEncoding = Encoding.UTF8;

BancoService banco = new BancoService();
MenuPrincipal menu = new MenuPrincipal(banco);

TelaCarregamento.Exibir();
menu.Executar();
