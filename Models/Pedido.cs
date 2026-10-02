using Microsoft.EntityFrameworkCore;

namespace implementation.Models;

public class Pedido
{
    public int Id { get; set; }
    // public DateTime Timestampt { get; set; } = new DateTime();
    public string NIF { get; set; } = "";
    public int Idade { get; set; } = 0;
    public decimal PrestacoesAtuais { get; set; } = 0;
    public decimal RendimentoMensal { get; set; } = 0;
    public decimal ValorPretendido { get; set; } = 0;
    public int Prazo { get; set; } = 0;
    public bool IncidentesCredito { get; set; } = false;
    public string SituacaoProfissional { get; set; } = "DESEMPREGADO";
    
    // É APROVADO porque esta decisão é a menos prioritária para ser display em comparação ao resto das decisões
    public string DecisaoFinal { get; set; } = "APROVADO";
    public List<string> Motivos { get; set; } = new();
    public decimal PrestacaoNova { get; set; } = 0;
    public decimal TaxaEsforco { get; set; } = 0;

    public static void CriarPedidoCredito()
    {
        Random rnd = new Random();
        
        int pedido = rnd.Next(10000000, 99999999);
        string decisaoFinal = "APROVADO";
        List<string> motivos = new();
        decimal taxaEsforco = 0;
        decimal prestacaoNova = 0;

        Console.Write("Insira o seu NIF: ");
        string nif = Console.ReadLine() ?? "";

        Console.Write("Insira a sua Idade: ");
        int idade;
        int.TryParse(Console.ReadLine(), out idade);

        Console.Write("Tem prestações atualmente, caso não insire 0: ");
        decimal prestacoesAtuais;
        decimal.TryParse(Console.ReadLine(), out prestacoesAtuais);

        Console.Write("Insira o seu rendimento mensal: ");
        decimal rendimentoMensal;
        decimal.TryParse(Console.ReadLine(), out rendimentoMensal);

        Console.Write("Insira o valor pretendido: ");
        decimal valorPretendido;
        decimal.TryParse(Console.ReadLine(), out valorPretendido);

        Console.Write("Insira o prazo: ");
        int prazo;
        int.TryParse(Console.ReadLine(), out prazo);

        Console.Write(
            """
            Teve incidentes de crédito:
            1. SIM
            2. NÃO

            """
        );
        Console.Write("Escolha a opção (1,2): ");
        string incidenteInput = Console.ReadLine() ?? "";
        bool incidente = false;
        switch (incidenteInput)
        {
            case "1":
                incidente = true;
                break;
            case "2":
                incidente = false;
                break;
            default:
                decisaoFinal = "PEDIDO INVÁLIDO";
                motivos.Add("A resposta só pode ser 1 ou 2.");
                break;
        }

        Console.WriteLine(
            """
            Qual a sua situação profissional:
            1. Efetivo
            2. Contratado a prazo
            3. Desempregado

            """
        );
        Console.Write("Escolha a opção (1,2,3): ");
        string situacaoProfissionalInput = Console.ReadLine() ?? "";
        string situacaoProfissional = "";
        switch (situacaoProfissionalInput)
        {
            case "1":
                situacaoProfissional = "EFETIVO";
                break;
            case "2":
                situacaoProfissional = "CONTRATO_PRAZO";
                break;
            case "3":
                situacaoProfissional = "DESEMPREGADO";
                break;
            default:
                decisaoFinal = "PEDIDO INVÁLIDO";
                motivos.Add("A resposta só pode ser 1, 2 ou 3.");
                break;
        }

        // Validacao inicial dos dados inseridos
        // Caso um falhe a decisao final é PEDIDO INVÁLIDO
        if (nif.Length is not 9)
        {
            decisaoFinal = "PEDIDO INVÁLIDO";
            motivos.Add("O NIF inserido não tem 9 dígitos.");
        }
        if (idade < 18)
        {
            decisaoFinal = "PEDIDO INVÁLIDO";
            motivos.Add("A idade inserida tem de ser igual ou superior a 18 anos.");
        }
        if (rendimentoMensal <= 0)
        {
            decisaoFinal = "PEDIDO INVÁLIDO";
            motivos.Add("O rendimento mensal ter de ser superior a 0.");
        }
        if (valorPretendido <= 0)
        {
            decisaoFinal = "PEDIDO INVÁLIDO";
            motivos.Add("O valor pretendiodo ter de ser superior a 0.");
        }
        if (prazo <= 0)
        {
            decisaoFinal = "PEDIDO INVÁLIDO";
            motivos.Add("O prazo ter de ser superior a 0 meses.");
        }

        if(decisaoFinal is not "PEDIDO INVÁLIDO")
        {
            // Incidentes de Crédito
            if (incidente)
            {
                decisaoFinal = "RECUSADO";
                motivos.Add("O cliente tem incidentes de crédito.");
            }
            // Situação Profissional
            if (situacaoProfissional == "DESEMPREGADO")
            {
                decisaoFinal = "RECUSADO";
                motivos.Add("O cliente está desempregado.");
            }
            
            // Taxa de Esforço
            prestacaoNova = valorPretendido/prazo;
            taxaEsforco = (prestacoesAtuais + prestacaoNova)/rendimentoMensal * 100;
            if (50 < taxaEsforco)
            {
                decisaoFinal = "RECUSADO";
                motivos.Add("A taxa de esforço excede os 50%.");
            }

            if (decisaoFinal is not "RECUSADO")
            {
                // Situação Profissional
                if(situacaoProfissional == "CONTRATO_PRAZO")
                {
                    decisaoFinal = "ANÁLISE MANUAL";
                    motivos.Add("O cliente está contratado a prazo.");
                }
                
                // Idade Máxima no Final do Contrato
                if (idade + prazo/12 > 75)
                {
                    decisaoFinal = "ANÁLISE MANUAL";
                    motivos.Add("A idade do cliente no final do contrato excede a idade máxima.");
                }

                // Limite do Montante
                if (valorPretendido/rendimentoMensal > 20)
                {
                    decisaoFinal = "ANÁLISE MANUAL";
                    motivos.Add("O valor pretendido excede 20 vezes o rendimento mensal líquido.");
                }

                if(35 < taxaEsforco && taxaEsforco <= 50)
                {
                    decisaoFinal = "ANÁLISE MANUAL";
                    motivos.Add("A taxa de esforço é superior a 35%.");
                }
            }
        }

        // Mostrar resultado ao cliente
        Console.WriteLine(
            $"""
            
            Pedido: {pedido}

            Decisão Final:
            {decisaoFinal}

            """
        );
        if (motivos.Count > 0)
        {
            Console.Write("Motivos:\n");
            motivos.ForEach(motivo => Console.WriteLine($" - {motivo}"));
        }
        Console.WriteLine(
            $"""
            Indicadores:
            - Taxa de Esforço: {Math.Round(taxaEsforco, 2)}%
            - Prestação Estimada: {Math.Round(prestacaoNova, 2)}€
            
            """
        );

        using (var db = new AppDbContext())
        {
            db.Pedidos.Add(new Pedido
                { 
                    NIF = nif,
                    Idade = idade,
                    PrestacoesAtuais = prestacoesAtuais,
                    RendimentoMensal = rendimentoMensal,
                    ValorPretendido = valorPretendido,
                    Prazo = prazo,
                    IncidentesCredito = incidente,
                    SituacaoProfissional = situacaoProfissional,
                    DecisaoFinal = decisaoFinal,
                    Motivos = motivos,
                    PrestacaoNova = prestacaoNova,
                    TaxaEsforco = taxaEsforco
                }
            );
            db.SaveChanges();
            Console.WriteLine("Pedido salvo com successo.\n");
        };
    }

    public static async Task ExtrairEstatisticasPedidos()
    {
        using (var db = new AppDbContext())
        {
            // Número de pedidos terminados com o estado APROVADO
            int numPedidosAprovados = db.Pedidos.Where(pedido => pedido.DecisaoFinal == "APROVADO").ToList().Count;
            
            // Número de pedidos terminados com cada um dos restantes estados para além do APROVADO
            int numPedidosNaoAprovados = db.Pedidos.Where(pedido => pedido.DecisaoFinal != "APROVADO").ToList().Count;
            
            // No caso de RECUSA, identificar qual o motivo mais frequente
            string? motivoFrequente = (await db.Pedidos
                .Where(pedido => pedido.DecisaoFinal == "RECUSADO")
                .Select(pedido => pedido.Motivos)
                .ToListAsync())
                .SelectMany(array => array)
                .GroupBy(str => str)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            // Obter clientes que realizaram mais do que um pedido/simulação no último mês
            var clientesMaisPedidosUltimoMes = await db.Pedidos
                .GroupBy(pedidos => pedidos.NIF)
                .Select(group => new
                {
                    NIF = group.Key,
                    NumPedidos = group.Count()
                })
                .Where(g => g.NumPedidos > 0)
                .ToListAsync();

            // Número de pedidos que terminaram com ANÁLISE MANUAL e que evoluíram para APROVADO
            // var numPedidosAnaliseParaAprovado = await db.Pedidos
            //    ;

            Console.WriteLine(
                $"""

                Exportado:

                 Nº Pedidos aprovados: {numPedidosAprovados}
                 Nº Pedidos não aprovados: {numPedidosNaoAprovados}
                 Motivo frequente: {motivoFrequente}

                """
            );

            Console.WriteLine(" Número de pedidos por cliente:");
            foreach (var cliente in clientesMaisPedidosUltimoMes)
            {
                Console.WriteLine($" - NIF: {cliente.NIF} | Pedidos: {cliente.NumPedidos}");
            };
            Console.WriteLine();


        };
    }
}