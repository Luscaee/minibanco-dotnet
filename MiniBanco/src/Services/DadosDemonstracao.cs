static class DadosDemonstracao
{
    public static bool JaCarregado { get; private set; } = false;

    public static void Carregar(BancoService banco)
    {
        long idCarla = banco.CriarContaPoupanca("Carla Mendes", 10, 0.8);
        long idBruno = banco.CriarContaCorrente("Bruno Lima", 1000m);
        long idDiego = banco.CriarContaCorrente("Diego Rocha", 1000m);
        long idEva = banco.CriarContaSalario("Eva Martins", "Padaria Central", 2);

        // Primeira rodada de movimentações
        banco.Depositar(idCarla, 5000m);
        banco.Depositar(idCarla, 1000m);
        banco.Sacar(idCarla, 300m);

        banco.Depositar(idBruno, 3000m);
        banco.Depositar(idBruno, 1000m);
        banco.Sacar(idBruno, 800m);

        // Diego usa o cheque especial
        banco.Depositar(idDiego, 200m);
        banco.Sacar(idDiego, 600m);
        banco.Sacar(idDiego, 300m);

        // O terceiro saque de Eva excede o limite e gera tarifa
        banco.Depositar(idEva, 2500m);
        banco.Sacar(idEva, 200m);
        banco.Sacar(idEva, 200m);
        banco.Sacar(idEva, 200m);

        banco.AplicarFimMes();

        // Segunda rodada de movimentações
        banco.Depositar(idBruno, 500m);
        banco.Depositar(idBruno, 700m);

        banco.Depositar(idEva, 1000m);
        banco.Depositar(idEva, 700m);

        banco.Depositar(idCarla, 500m);
        banco.Transferir(idBruno, idCarla, 200m);

        banco.AplicarFimMes();

        banco.Sacar(idEva, 200m);

        JaCarregado = true;
    }
}