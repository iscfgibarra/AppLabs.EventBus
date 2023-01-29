using Xunit.Abstractions;

namespace AppLabs.EventBus.Test.Events;


/// <summary>
/// Esta clase permite ilustrar que los suscriptores pueden recibir parametros y configuraciones por medio de esta clase al iniciarla
/// </summary>
public class Callback
{
    public ITestOutputHelper OutputHelper { get; set; }
}