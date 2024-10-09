class Program
{
    static readonly Random random = new Random();

    static async Task Main(string[] args)
    {
        int numberOfOrders = 5; 
        List<Task> tasks = new List<Task>();
        int completedTasks = 0;
        object lockObject = new object();

        for (int i = 1; i <= numberOfOrders; i++)
        {
            int orderId = i;
            tasks.Add(PrepareAsync(orderId, () => UpdateProgress(ref completedTasks, numberOfOrders, lockObject, orderId)));
        }

        await Task.WhenAll(tasks); 

        Console.WriteLine("\nTodos os pedidos foram processados. Obrigado pela sua paciência!");
    }

    static async Task PrepareAsync(int orderId, Action onComplete)
    {
        int delay = random.Next(1, 5) * 1000;

        await Task.Delay(delay);

        onComplete(); 

        Console.WriteLine($"Pedido ID: {orderId} preparado, Tempo de execução: {delay / 1000} segundos");
    }

    static void UpdateProgress(ref int completed, int total, object lockObject, int orderId)
    {
        int currentCompleted;
        lock (lockObject) 
        {
            completed++;
            currentCompleted = completed;
        }

        double percent = (double)currentCompleted / total * 100;

        Console.CursorLeft = 0; 
        Console.Write($"Processando Pedido ID: {orderId}... {currentCompleted}/{total} ({percent:F0}%)\r");
    }
}
