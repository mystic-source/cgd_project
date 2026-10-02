using implementation.Models;

bool running = true;

Console.WriteLine("Bem vindo!");
Console.WriteLine("==========================================");

while(running)
{
    Console.WriteLine(
        """
        Lista de opções:
        1. Criar pedido de crédito
        2. Extrair estatisticas
        """
    );
    Console.Write("Escolha uma opção: ");
    string opcao = Console.ReadLine() ?? "";

    switch(opcao)
    {
        case "1":
            Console.WriteLine(
                """
                Para fazer um pedido de crédito, vai precisar de inserir:
                - NIF
                - Idade
                - Rendimento mensal
                - Valor pretendido
                - Prazo
                - IncidentesCredito
                - SituacaoProfissional
                """
            );
            Pedido.CriarPedidoCredito();
            break;
        case "2":
            await Pedido.ExtrairEstatisticasPedidos();
            break;
        default:
            break;
    }
}