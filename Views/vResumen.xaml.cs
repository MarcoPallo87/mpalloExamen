using System.Text;
namespace mpalloExamen.Views;

public partial class vResumen : ContentPage
{
    // Recepcion de los datos del Registro
    public string UsuarioConectado { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Edad { get; set; } = string.Empty;
    public string FechaInicio { get; set; } = string.Empty;
    public string Plan { get; set; } = string.Empty;
    public string Casillero { get; set; } = string.Empty;
    public double MontoInicial { get; set; }
    public double PagoMensual { get; set; }
    public double PagoTotal { get; set; }

    public vResumen()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Llenamos los Labels con los datos recibidos
        lblUsuarioConectado.Text = $"Usuario conectado: {UsuarioConectado}";
        lblNombre.Text = Nombre;
        lblApellido.Text = Apellido;
        lblEdad.Text = Edad;
        lblFechaInicio.Text = FechaInicio;
        lblPlan.Text = Plan;
        lblCasillero.Text = Casillero;
        lblMontoInicial.Text = $"${MontoInicial:F2}";
        lblPagoMensual.Text = $"${PagoMensual:F2}";
        lblPagoTotal.Text = $"${PagoTotal:F2}";
    }

    private async void OnNuevoRegistroClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnCerrarSesionClicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void OnExportarClicked(object? sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("===== RESUMEN DE SOCIO =====");
        sb.AppendLine($"Usuario conectado: {UsuarioConectado}");
        sb.AppendLine($"Nombre: {Nombre}");
        sb.AppendLine($"Apellido: {Apellido}");
        sb.AppendLine($"Edad: {Edad}");
        sb.AppendLine($"Fecha de inicio: {FechaInicio}");
        sb.AppendLine($"Plan: {Plan}");
        sb.AppendLine($"Casillero: {Casillero}");
        sb.AppendLine($"Monto Inicial: ${MontoInicial:F2}");
        sb.AppendLine($"Pago mensual: ${PagoMensual:F2}");
        sb.AppendLine($"Pago Total: ${PagoTotal:F2}");

        string nombreArchivo = $"Resumen_{Nombre}_{Apellido}.txt";
        string ruta = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);

        File.WriteAllText(ruta, sb.ToString());

        await DisplayAlertAsync("Éxito", $"Archivo guardado: {nombreArchivo}\nRuta: {ruta}", "OK");
    }
}