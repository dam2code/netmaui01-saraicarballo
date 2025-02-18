namespace People;

public partial class App : Application
{
    // Propiedad estática para acceder a PersonRepository
    public static PersonRepository PersonRepo { get; private set; }

    public App(PersonRepository repo)
    {
        InitializeComponent();
        // Inicializar PersonRepo con el objeto PersonRepository inyectado
        PersonRepo = repo;
    }

    // Crear la ventana principal de la aplicación
    protected override Window CreateWindow(IActivationState activationState)
    {
        return new Window(new AppShell());
    }
}
